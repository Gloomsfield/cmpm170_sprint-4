using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // Allows any script to call AudioManager.Instance.PlaySound()
    public static AudioManager Instance { get; private set; }

    // This creates the clean row structure inside your inspector
    [System.Serializable]
    public struct SoundEvent
    {
        public string eventName;      // e.g., "BallInteract", "PlayerJump"
        public AudioClip clip;        // The audio file itself
        [Range(0f, 1f)] public float volume;
        [Range(0.5f, 2f)] public float pitch;
    }

    [Header("Audio Settings")]
    [SerializeField] private int audioSourcePoolSize = 5;
    [SerializeField] private List<SoundEvent> soundEvents; // Your big list in the Inspector

    private Dictionary<string, SoundEvent> _soundDictionary;
    private List<AudioSource> _pool;

    private void Awake()
    {
        // Setup Singleton
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        // Convert the list to a dictionary for lightning-fast lookups by name
        _soundDictionary = new Dictionary<string, SoundEvent>();
        foreach (var sound in soundEvents)
        {
            if (!string.IsNullOrEmpty(sound.eventName) && !_soundDictionary.ContainsKey(sound.eventName))
            {
                _soundDictionary.Add(sound.eventName, sound);
            }
        }

        InitializePool();
    }

    private void InitializePool()
    {
        _pool = new List<AudioSource>();
        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _pool.Add(source);
        }
    }

    // The single function your other objects will call
    public void PlaySound(string eventName)
    {
        // Look up the sound definition once by name
        if (!_soundDictionary.TryGetValue(eventName, out SoundEvent sound))
        {
            Debug.LogWarning($"Audio Manager: Sound event '{eventName}' not found!");
            return;
        }

        // Reuse an idle AudioSource from the pool
        AudioSource availableSource = GetAvailableSource();
        if (availableSource == null) return;

        // Apply settings directly from your inspector configuration
        availableSource.clip = sound.clip;
        availableSource.volume = sound.volume == 0 ? 1f : sound.volume; // Default to 1 if left at 0
        availableSource.pitch = sound.pitch == 0 ? 1f : sound.pitch;   // Default to 1 if left at 0
        availableSource.Play();
    }

    private AudioSource GetAvailableSource()
    {
        foreach (var source in _pool)
        {
            if (!source.isPlaying) return source;
        }
        // If all sources are busy, grab the first one or expand pool
        return _pool[0]; 
    }
}
