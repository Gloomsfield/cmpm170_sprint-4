using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody ballRigidbody;

    [Header("Ball Settings")]
    [SerializeField] float shootForce = 10.0f;
    [SerializeField] float rotationForce = 5.0f;
    float rotation;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        EventManager.shootBall += ShootBall;
        EventManager.rotation += GetRotation;
        EventManager.rotationAD += GetRotationAD;
        EventManager.reset += ResetBall;
    }

    void OnDisable()
    {
        EventManager.shootBall -= ShootBall;
        EventManager.rotation -= GetRotation;
        EventManager.rotationAD -= GetRotationAD;
        EventManager.reset -= ResetBall;
    }

    void ShootBall()
    {
        Debug.Log("Ball shot!");
        GetComponent<Rigidbody>().AddForce(0, 0, shootForce, ForceMode.Impulse);
    }

    void GetRotation(float rotationValue)
    {
        rotation = rotationValue;
        Debug.Log("Ball rotated: " + rotationValue);
    }

    void GetRotationAD(float rotationValue)
    {
        rotation = rotationValue;
        Debug.Log("Ball rotated AD: " + rotationValue);
    }

    void ResetBall()
    {
        Debug.Log("Ball reset!");
        ballRigidbody.linearVelocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 6, 13);
    }

    void Update()
    {
        ApplyRotation();
    }

    void ApplyRotation()
    {
        transform.Translate(new Vector3(rotation * rotationForce, 0, 0) * Time.deltaTime);
    }
}
