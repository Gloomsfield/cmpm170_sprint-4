using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmbienceManager : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] List<Light> ambienceLights;

    [Header("Colors")]
    [SerializeField] Color normalColor = new Color(1f, 0.7f, 0.3f);

    [SerializeField] Color bloodLustColor = Color.red;

    [Header("Transition")]
    [SerializeField] float transitionDuration = 2f;

    Coroutine currentTransition;

    void Start()
    {
        SetAllLights(normalColor);
    }

    void OnEnable()
    {
        EventManager.redLights += TriggerRed;
        EventManager.normalLights += ReturnToNormal;
    }

    void OnDisable()
    {
        EventManager.redLights -= TriggerRed;
        EventManager.normalLights -= ReturnToNormal;
    }

    void TriggerRed()
    {
        ChangeLighting(bloodLustColor);
    }

    public void ReturnToNormal()
    {
        ChangeLighting(normalColor);
    }

    void ChangeLighting(Color targetColor)
    {
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }
        currentTransition = StartCoroutine(TransitionLights(targetColor));
    }

    IEnumerator TransitionLights(Color targetColor)
    {
        float timer = 0f;
        Color startColor = ambienceLights[0].color;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            Color currentColor = Color.Lerp(startColor, targetColor, timer / transitionDuration);
            SetAllLights(currentColor);

            yield return null;
        }
        SetAllLights(targetColor);
    }

    void SetAllLights(Color color)
    {
        foreach (var lightSource in ambienceLights)
        {
            lightSource.color = color;
        }
    }
}