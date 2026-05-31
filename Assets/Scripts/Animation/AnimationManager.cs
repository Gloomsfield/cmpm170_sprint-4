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
    [SerializeField] ScreenFader screenFader;

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
        StopAllCoroutines();
        AudioManager.Instance.StopAllSounds();
        StartCoroutine(PlayAnimationCoroutine(animationDictionary[animationName]));
    }

    IEnumerator PlayAnimationCoroutine(ScreenAnimation animation)
    {
        bool firstFrame = true;
        foreach (var frame in animation.frames)
        {
            if (firstFrame)
            {
                screenRenderer.material.SetTexture("_BaseMap", frame.image);
                yield return StartCoroutine(screenFader.FadeFromBlack(0.7f));
                firstFrame = false;
            }
            else
            {
                yield return StartCoroutine(screenFader.FadeToBlack(0.7f));
                screenRenderer.material.SetTexture("_BaseMap", frame.image);
                yield return StartCoroutine(screenFader.FadeFromBlack(0.7f));
            }

            foreach (var sound in frame.sounds)
            {
                StartCoroutine(PlayDelayedSound(sound));
            }

            yield return new WaitForSeconds(frame.duration);
        }

        yield return StartCoroutine(screenFader.FadeToBlack(0.7f));

        if (animation.animationName == "Fire" || animation.animationName == "Boulder") 
        {
            GameManager.Instance.UnlockBallHolders();
        }
    }

    IEnumerator PlayDelayedSound(SoundCue soundCue)
    {
        yield return new WaitForSeconds(soundCue.delay);
        AudioManager.Instance.PlaySound(soundCue.soundName);
    }
}