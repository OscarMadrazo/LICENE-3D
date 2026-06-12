using UnityEngine;

public class MenuControllerAnimation : MonoBehaviour
{
    public RectTransform title; // UI title
    public GameObject pressAnyButton; // Texto "Presiona cualquier botón"
    public CanvasGroup backgroundExtra; // Fondo que se desvanecerá

    public GameObject canvasIntro; // <- REFERENCIA al canvas a desactivar

    private Vector2 titleStartPos;
    public Vector2 titleTargetPos = new Vector2(-11, 90);

    public float moveSpeed = 2f;
    public float fadeSpeed = 1.5f;

    private bool isMoving = false;
    private bool startFadeOut = false;

    void Start()
    {
        titleStartPos = title.anchoredPosition;
        backgroundExtra.alpha = 1f;

        pressAnyButton.SetActive(true);
    }

    void Update()
    {
        if (!isMoving && Input.anyKeyDown)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            // Mover el título
            title.anchoredPosition = Vector2.Lerp(title.anchoredPosition, titleTargetPos, Time.deltaTime * moveSpeed);

            // Desactivar el texto si ya está casi en la posición final
            if (Vector2.Distance(title.anchoredPosition, titleTargetPos) < 0.1f && !startFadeOut)
            {
                pressAnyButton.SetActive(false);
                startFadeOut = true;
            }
        }

        if (startFadeOut)
        {
            // Desvanecer el título
            CanvasGroup titleCanvas = title.GetComponent<CanvasGroup>();
            if (titleCanvas != null)
            {
                titleCanvas.alpha -= Time.deltaTime * fadeSpeed;
            }

            // Desvanecer fondo extra
            backgroundExtra.alpha -= Time.deltaTime * fadeSpeed;

            // Verificar si ambos se desvanecieron completamente
            if ((titleCanvas == null || titleCanvas.alpha <= 0f) && backgroundExtra.alpha <= 0f)
            {
                if (titleCanvas != null) titleCanvas.alpha = 0f;
                backgroundExtra.alpha = 0f;

                // 🔴 Desactiva el canvas de introducción completamente
                if (canvasIntro != null)
                {
                    canvasIntro.SetActive(false);
                }

                startFadeOut = false;
            }
        }
    }
}
