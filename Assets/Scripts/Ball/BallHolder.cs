using UnityEngine;

public class BallHolder : MonoBehaviour
{

    [Header("Hold Settings")]
    [SerializeField] Transform holdPosition;
    [SerializeField] Transform releasePosition;

    Ball heldBall;
    Rigidbody heldBallRigidbody;

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
}
