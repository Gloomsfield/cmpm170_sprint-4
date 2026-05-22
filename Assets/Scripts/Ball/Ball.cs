using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    Rigidbody ballRigidbody;

    [Header("Ball Settings")]
    [SerializeField] float shootForce = 10.0f;
    [SerializeField] float rotationForce = 5.0f;
    [SerializeField] float ballTippingTime = 2.0f;
    float rotation;
    bool ballTipping = false;
    bool ballTippedTooMuch = false;
    Coroutine tippingCoroutine;

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
        ballRigidbody.AddForce(0, 0, shootForce, ForceMode.Impulse);
    }

    void GetRotation(float rotationValue)
    {
        rotation = rotationValue;

        if (!ballTipping && (rotationValue < -0.1f || rotationValue > 0.1f))
        {
            //Debug.Log("Ball should BE tipping");
            ballTipping = true;
            tippingCoroutine = StartCoroutine(BallTipToMuch());
        }
        else if (ballTipping && (rotationValue > -0.1f && rotationValue < 0.1f))
        {
            //Debug.Log("Ball should not be tipping");
            ballTipping = false;

            if (tippingCoroutine != null)
            {
                StopCoroutine(tippingCoroutine);
                tippingCoroutine = null;
            }
        }

        //Debug.Log("Ball rotated: " + rotationValue);
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
        transform.position = new Vector3(0, 6, 13);
        ballTippedTooMuch = false;
    }

    void Update()
    {
        if (!ballTippedTooMuch)
        {
            ApplyRotation();
        }
;
    }

    void ApplyRotation()
    {
        transform.Translate(new Vector3(rotation * rotationForce, 0, 0) * Time.deltaTime);
    }

    IEnumerator BallTipToMuch()
    {
        ballTipping = true;
        yield return new WaitForSeconds(ballTippingTime);
        // TODO: Change this to a kill pinball function or something that stops all controls and resets the ball after it falls off the table
        ballTippedTooMuch = true;
        Debug.Log("Ball tipped too much!");
    }
}
