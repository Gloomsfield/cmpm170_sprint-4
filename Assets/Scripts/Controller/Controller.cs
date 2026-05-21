using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{

    [SerializeField] private ControllerInputHandler controllerInputHandler; 

    void OnShootBall(InputValue value)
    {
        //TODO: Add event to shoot ball
        Debug.Log("ShootBall: " + value.Get<float>());
    }

    void OnLeftPaddle(InputValue value)
    {
        //TODO: Add event to move left paddle
        Debug.Log("Left Paddle: " + value.Get<float>());
    }

    void OnRightPaddle(InputValue value)
    {
        //TODO: Add event to move right paddle
        Debug.Log("Right Paddle: " + value.Get<float>());
    }

    void OnRotationAD(InputValue value)
    {
        //TODO: Add event to handle rotation AD input
        Debug.Log("Rotation AD: " + value.Get<float>());
    }

    void OnRotation(InputValue value)
    {
        //TODO: Add event to handle rotation input
        Debug.Log("Rotation: " + value.Get<float>());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
