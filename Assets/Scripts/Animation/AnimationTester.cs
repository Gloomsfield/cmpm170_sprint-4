using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationTester : MonoBehaviour
{
    void Update()
    {
        // Press 1 to test fire animation
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            EventManager.InvokePlayAnimation("Fire");

            Debug.Log("Played Fire");
        }

        // Press 2 to test boulder animation
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            EventManager.InvokePlayAnimation("Boulder");

            Debug.Log("Played Boulder");
        }
    }
}