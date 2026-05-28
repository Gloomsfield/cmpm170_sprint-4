using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimationFrame
    {
        public Texture image;
        public float duration = 2f;
        public List<string> soundsToPlay;
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
            screenRenderer.material.mainTexture = frame.image;
            foreach (var soundName in frame.soundsToPlay)
            {
                AudioManager.Instance.PlaySound(soundName);
            }

            yield return new WaitForSeconds(frame.duration);
        }
    }
}