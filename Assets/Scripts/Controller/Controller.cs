using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{

    //[SerializeField] private ControllerInputHandler controllerInputHandler; 


    void OnShootBall(InputValue value)
    {
        EventManager.InvokeShootBall();
        //Debug.Log("ShootBall: " + value.Get<float>());
    }

    void OnLeftPaddle(InputValue value)
    {
        //TODO: Add event to move left paddle
        Debug.Log("Left Paddle: " + value.Get<float>());
        EventManager.InvokeLeftFlipperTriggered();
    }

    void OnRightPaddle(InputValue value)
    {
        //TODO: Add event to move right paddle
        Debug.Log("Right Paddle: " + value.Get<float>());
        EventManager.InvokeRightFlipperTriggered();
    }

    void OnRotationAD(InputValue value)
    {
        //TODO: Add event to handle rotation AD input
        //Debug.Log("Rotation AD: " + value.Get<float>());
        EventManager.InvokeRotationAD(value.Get<float>());
    }

    void OnRotation(InputValue value)
    {
        //TODO: Add event to handle rotation input
        //Debug.Log("Rotation: " + value.Get<float>());
        EventManager.InvokeRotation(value.Get<float>());
    }

    void OnReset()
    {
        EventManager.InvokeReset();
    }
}
