using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationTester : MonoBehaviour
{
    void Update()
    {
        // Press z to test fire animation
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            EventManager.InvokePlayAnimation("Fire");

            Debug.Log("Played Fire");
        }

        // Press x to test boulder animation
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            EventManager.InvokePlayAnimation("Boulder");

            Debug.Log("Played Boulder");
        }

        // Press o to test boulder animation
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            EventManager.InvokePlayReflectionAnimation("BoarFace");

            Debug.Log("Played Boar");
        }

        // Press L to test boulder animation
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            EventManager.InvokeRedLights();
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            EventManager.InvokeNormalLights();
        }
    }
}

// ALL Event calls I made:

/*
EventManager.InvokePlayAnimation("Fire");
animation for bonfire

EventManager.InvokePlayAnimation("Boulder");
animation for boulder

EventManager.InvokePlayReflectionAnimation("BoarFace");
animation for happy boar (good score)

EventManager.InvokePlayReflectionAnimation("BoarFace");
animation for angry boar (lose ball)

EventManager.InvokePlayReflectionAnimation("BoarFace");
animation for boar speaking, whenever u want him to speak idk

EventManager.InvokeRedLights();
// red lights on when bloodlust mode (fire animation and x2 points)

EventManager.InvokeNormalLights();
// normal lights for red lights off
*/