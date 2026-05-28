using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour
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
        public List<SoundCue> sounds;
    }

    [System.Serializable]
    public class ScreenAnimation
    {
        public string animationName;
        public List<AnimationFrame> frames;
    }

    [Header("Screen")]
    [SerializeField] Renderer screenRenderer;

    [Header("Animations")]
    [SerializeField] List<ScreenAnimation> animations;

    Dictionary<string, ScreenAnimation> animationDictionary;

    void Awake()
    {
        animationDictionary = new Dictionary<string, ScreenAnimation>();
        foreach (var animation in animations)
        {
            animationDictionary.Add(animation.animationName, animation);
        }
    }

    void OnEnable()
    {
        EventManager.playAnimation += PlayAnimation;
    }

    void OnDisable()
    {
        EventManager.playAnimation -= PlayAnimation;
    }

    void PlayAnimation(string animationName)
    {
        if (!animationDictionary.ContainsKey(animationName))
        {
            Debug.LogWarning("Animation not found: " + animationName);
            return;
        }

        StopAllCoroutines();
        StartCoroutine(PlayAnimationCoroutine(animationDictionary[animationName]));
    }

    IEnumerator PlayAnimationCoroutine(ScreenAnimation animation)
    {
        foreach (var frame in animation.frames)
        {
            screenRenderer.material.SetTexture("_BaseMap", frame.image);
            foreach (var sound in frame.sounds)
            {
                StartCoroutine(PlayDelayedSound(sound));
            }

            yield return new WaitForSeconds(frame.duration);
        }
    }

    IEnumerator PlayDelayedSound(SoundCue soundCue)
    {
        yield return new WaitForSeconds(soundCue.delay);
        AudioManager.Instance.PlaySound(soundCue.soundName);
    }
}