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
        //Debug.Log("Left Paddle: " + value.Get<float>());
        EventManager.InvokeLeftFlipperTriggered();
    }

    void OnRightPaddle(InputValue value)
    {
        //TODO: Add event to move right paddle
        //Debug.Log("Right Paddle: " + value.Get<float>());
        EventManager.InvokeRightFlipperTriggered();
    }

    void OnRotationAD(InputValue value)
    {
        //TODO: Add event to handle rotation AD input
        //Debug.Log("Rotation AD: " + value.Get<float>());
        if (!GameManager.Instance.CanTilt) return;
        EventManager.InvokeNudgeBall(value.Get<float>());
    }

    void OnRotation(InputValue value)
    {
        //TODO: Add event to handle rotation input
        //Debug.Log("Rotation: " + value.Get<float>());
        if (!GameManager.Instance.CanTilt) return;
        float mouseAmount = value.Get<float>() * 0.25f;
        EventManager.InvokeRotation(mouseAmount);
        //EventManager.InvokeRotation(value.Get<float>());
    }

    /*void OnReset()
    {
        EventManager.InvokeReset();
    }*/

    void OnSpawnBall()
    {
        EventManager.InvokeSpawnBall();
    }

    void OnPauseMenu()
    {
        if (GameManager.Instance.State == GameManager.GameState.PREGAME || GameManager.Instance.State == GameManager.GameState.ENDGAME ) return;
        EventManager.InvokePauseMenu();
    }
}
