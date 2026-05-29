using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] Renderer fadeRenderer;
    Material fadeMaterial;

    void Awake()
    {
        fadeMaterial = fadeRenderer.material;
    }

    public IEnumerator FadeToBlack(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = timer / duration;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1f);
    }

    public IEnumerator FadeFromBlack(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = 1f - (timer / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        Color color = fadeMaterial.color;
        color.a = alpha;
        fadeMaterial.color = color;
    }
}