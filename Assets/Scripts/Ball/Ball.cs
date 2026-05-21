using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.shootBall += ShootBall;
        EventManager.rotation += ApplyRotation;
        EventManager.rotationAD += ApplyRotationAD;
    }

    void OnDisable()
    {
        EventManager.shootBall -= ShootBall;
        EventManager.rotation -= ApplyRotation;
        EventManager.rotationAD -= ApplyRotationAD;
    }

    void ShootBall()
    {
        Debug.Log("Ball shot!");

    }

    void ApplyRotation(float rotationValue)
    {
        Debug.Log("Ball rotated: " + rotationValue);
    }

    void ApplyRotationAD(float rotationValue)
    {
        Debug.Log("Ball rotated AD: " + rotationValue);
    }
}
