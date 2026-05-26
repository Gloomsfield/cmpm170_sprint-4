using UnityEngine;
using System.Collections;

public class BallHolder : MonoBehaviour
{

    [Header("Hold Positions")]
    [SerializeField] Transform holdPosition;
    [SerializeField] Transform releasePosition;

    [Header("Holder Settings")]
    [SerializeField] int hitsRequired = 2;

    int currentHits = 0;
    Ball heldBall;
    Rigidbody heldBallRigidbody;

    Coroutine TestTimer;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cave triggered");
        Ball ball = other.GetComponent<Ball>();

        if (ball == null) return;
        if (heldBall != null) return;

        currentHits++;

        if (currentHits >= hitsRequired)
        {
            currentHits =0;
            HoldBall(ball);
            TestTimer = StartCoroutine(TestHoldDuration());
        }

        // TODO: Add event to play UI and stop controller input
    }

    public void HoldBall(Ball ball)
    {
        if (heldBall != null) return;

        heldBall = ball;
        heldBallRigidbody = heldBall.GetComponent<Rigidbody>();

        heldBallRigidbody.linearVelocity = Vector3.zero;
        heldBallRigidbody.angularVelocity = Vector3.zero;
        heldBallRigidbody.isKinematic = true;

        heldBall.transform.position = holdPosition.position;
        heldBall.transform.rotation = holdPosition.rotation;

        Debug.Log("Ball held!");
    }

    public void ReleaseBall()
    {
        if (heldBall == null) return;

        heldBall.transform.position = releasePosition.position;
        heldBall.transform.rotation = releasePosition.rotation;
        heldBallRigidbody.isKinematic = false;

        Debug.Log("Ball released!");

        heldBall = null;
        heldBallRigidbody = null;
    }

    IEnumerator TestHoldDuration()
    {
        yield return new WaitForSeconds(5f);
        ReleaseBall();
    }

}
