using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimationFrame
    {
      
    }

    [System.Serializable]
    public class ScreenAnimation
    {
       
    }

    [Header("Screen")]
    [SerializeField] Renderer screenRenderer;

    [Header("Animations")]
    [SerializeField] List<ScreenAnimation> animations;

    Dictionary<string, ScreenAnimation> animationDictionary;

    void Awake()
    {
        
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
       
    }

    void PlayAnimation(string animationName)
    {

    }

    IEnumerator PlayAnimationCoroutine(ScreenAnimation animation)
    {

    }
}