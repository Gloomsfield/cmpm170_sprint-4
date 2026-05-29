using UnityEngine;
using System.Collections;

public class PiggyLight : MonoBehaviour
{
    [SerializeField] private Light targetLight;

    [Header("Flicker Settings")]
    [SerializeField] private float flickerDuration = 5f;
    [SerializeField] private float flickerInterval = 0.5f;

    private Coroutine flickerCoroutine;

    void OnEnable()
    {
        EventManager.startFlicker += StartFlicker;
    }

    void OnDisable()
    {
        EventManager.startFlicker -= StartFlicker;
    }

    public void StartFlicker()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
        }

        flickerCoroutine = StartCoroutine(FlickerCoroutine());
    }

    private IEnumerator FlickerCoroutine()
    {
        float timer = 0f;

        while (timer < flickerDuration)
        {
            targetLight.enabled = !targetLight.enabled;

            yield return new WaitForSeconds(flickerInterval);
            timer += flickerInterval;
        }

        targetLight.enabled = false;
        flickerCoroutine = null;
    }
}
