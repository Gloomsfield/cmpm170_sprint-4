using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReflectionAnimationManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundCue
    {
        public string soundName;
        public float delay = 0f;
    }

    [System.Serializable]
    public class AnimationFrame
    {
        public Texture image;
        public float duration = 2f;
        public float fadeDuration = 0.5f;
        public List<SoundCue> sounds;
    }

    [System.Serializable]
    public class ScreenAnimation
    {
        public string animationName;
        public List<AnimationFrame> frames;
    }

    [Header("Reflection Screen")]
    [SerializeField] Renderer screenRenderer;

    [Header("Animations")]
    [SerializeField] List<ScreenAnimation> animations;

    Dictionary<string, ScreenAnimation> animationDictionary;
    Material screenMaterial;

    void Awake()
    {
        screenMaterial = screenRenderer.material;
        animationDictionary = new Dictionary<string, ScreenAnimation>();

        foreach (var animation in animations)
        {
            animationDictionary.Add(animation.animationName, animation);
        }
        SetAlpha(0f);
    }

    void OnEnable()
    {
        EventManager.playReflectionAnimation += PlayAnimation;
    }

    void OnDisable()
    {
        EventManager.playReflectionAnimation -= PlayAnimation;
    }

    void PlayAnimation(string animationName)
    {
        StopAllCoroutines();
        StartCoroutine(PlayAnimationCoroutine(animationDictionary[animationName]));
    }

    IEnumerator PlayAnimationCoroutine(ScreenAnimation animation)
    {
        foreach (var frame in animation.frames)
        {
            screenMaterial.SetTexture("_BaseMap", frame.image);
            yield return StartCoroutine(FadeAlpha(0f, .6f, frame.fadeDuration));

            foreach (var sound in frame.sounds)
            {
                StartCoroutine(PlayDelayedSound(sound));
            }

            yield return new WaitForSeconds(frame.duration);
            yield return StartCoroutine(FadeAlpha(.6f, 0f, frame.fadeDuration));
        }
    }

    IEnumerator PlayDelayedSound(SoundCue soundCue)
    {
        yield return new WaitForSeconds(soundCue.delay);
        AudioManager.Instance.PlaySound(soundCue.soundName);
    }

    IEnumerator FadeAlpha(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha,endAlpha,timer / duration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(endAlpha);
    }

    void SetAlpha(float alpha)
    {
        Color color = screenMaterial.color;
        color.a = alpha;
        screenMaterial.color = color;
    }
}