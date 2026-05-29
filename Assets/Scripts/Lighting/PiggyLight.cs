using UnityEngine;
using System.Collections;

public class PiggyLight : MonoBehaviour
{
    [SerializeField] private Light targetLight;

    [Header("Flicker Timing")]
    [SerializeField] private float flickerDuration = 5f;
    [SerializeField] private float minFlickerInterval = 0.03f;
    [SerializeField] private float maxFlickerInterval = 0.35f;

    [Header("Light Intensity")]
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 2.5f;

    [Header("End State")]
    [SerializeField] private bool lightOnAfterFlicker = false;

    private Coroutine flickerCoroutine;
    private float originalIntensity;

    void OnEnable()
    {
        EventManager.flickerLight += StartFlicker;
    }

    void OnDisable()
    {
        EventManager.flickerLight -= StartFlicker;
    }

    private void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        if (targetLight != null)
        {
            originalIntensity = targetLight.intensity;

            // Light starts OFF, but the GameObject/script stays active
            targetLight.enabled = false;
        }
    }

    public void StartFlicker()
    {
        if (targetLight == null)
        {
            Debug.LogWarning("No target light assigned.");
            return;
        }

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
            targetLight.enabled = Random.value > 0.25f;
            targetLight.intensity = Random.Range(minIntensity, maxIntensity);

            float randomWait = Random.Range(minFlickerInterval, maxFlickerInterval);

            yield return new WaitForSeconds(randomWait);
            timer += randomWait;
        }

        targetLight.intensity = originalIntensity;
        targetLight.enabled = lightOnAfterFlicker;

        flickerCoroutine = null;
    }
}
