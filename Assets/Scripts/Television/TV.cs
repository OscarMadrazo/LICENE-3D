using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(SphereCollider))]
public class TV : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private VideoPlayer video;
    [SerializeField] private GameObject Quad;
    [SerializeField] private GameObject Quad1;

    [Header("Configuración")]
    [SerializeField] private float triggerRadius = 0.25f;

    private double pausedTime = 0;
    private bool playerIsInRange;

    private void Awake()
    {
        SphereCollider sphere = GetComponent<SphereCollider>();
        sphere.radius = triggerRadius;
        sphere.isTrigger = true;

        if (video != null)
        {
            video.playOnAwake = false;
            video.waitForFirstFrame = true;
        }

        if (Quad != null) Quad.SetActive(true);
        if (Quad1 != null) Quad1.SetActive(true);
    }

    private void OnEnable()
    {
        if (video != null)
            video.loopPointReached += OnVideoFinished;
    }

    private void OnDisable()
    {
        if (video != null)
            video.loopPointReached -= OnVideoFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerIsInRange = true;
        ActivateQuad();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerIsInRange = false;
        DeactivateQuad();
    }

    private void ActivateQuad()
    {
        if (Quad != null) Quad.SetActive(true);

        if (video != null)
        {
            video.time = pausedTime;
            video.Play();
        }
    }

    private void DeactivateQuad()
    {
        if (video != null && video.isPlaying)
        {
            pausedTime = video.time;
            video.Pause();
        }

        if (Quad != null) Quad.SetActive(false);
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        pausedTime = 0;

        if (playerIsInRange)
        {
            video.time = 0;
            video.Play(); 
        }
    }
}
