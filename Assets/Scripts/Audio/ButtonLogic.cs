using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundButton : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("Type the exact eventName from your AudioManager list here.")]
    public string soundEventName;

    private void Awake()
    {
        // Automatically listen to the button click event
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (AudioManager.Instance != null && !string.IsNullOrEmpty(soundEventName))
        {
            AudioManager.Instance.PlaySound(soundEventName);
        }
        else
        {
            Debug.LogWarning($"Cannot play sound. AudioManager instance missing or sound name empty on {gameObject.name}");
        }
    }
}
