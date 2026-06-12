using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.15f; // velocidad de fade (rápido)
    public Vector3 scaleFrom = new Vector3(0.8f, 0.8f, 0.8f); // inicio pequeño
    public Vector3 scaleTo = Vector3.one; // tamaño normal

    float targetAlpha;
    bool isAnimating = false;

    void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        transform.localScale = scaleFrom;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(1f));
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(0f));
    }

    System.Collections.IEnumerator FadeCanvas(float target)
    {
        isAnimating = true;
        float startAlpha = canvasGroup.alpha;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = (target > 0.5f) ? scaleTo : scaleFrom;

        float t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, target, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        canvasGroup.alpha = target;
        transform.localScale = endScale;

        if (target < 0.5f) gameObject.SetActive(false);
        isAnimating = false;
    }
}
