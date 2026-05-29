using UnityEngine;
using System.Collections;

public class BallHolder : MonoBehaviour
{

    [Header("Hold Positions")]
    [SerializeField] Transform holdPosition;
    [SerializeField] Transform releasePosition;

    [Header("Holder Settings")]
    [SerializeField] int hitsRequired = 2;
    [SerializeField] float holdDuration = 5.0f;
    [SerializeField] float canTriggerDelay = 3.0f;
    [SerializeField] float delayBetweenSpawns = 1.0f;
    [SerializeField] float animationDelay;

    [Header("Balls to Spawn")]
    [SerializeField] int ballsToSpawn = 2;

    [Header("Object Name")]
    [SerializeField] string objectName;
    bool canTrigger = true;
    int currentHits = 0;
    Ball heldBall;
    Rigidbody heldBallRigidbody;

    void OnTriggerEnter(Collider other)
    {
        if (!canTrigger) return;

        Debug.Log("Cave triggered");
        Ball ball = other.GetComponent<Ball>();

        if (ball == null) return;
        if (heldBall != null) return;

        currentHits++;

        if (currentHits >= hitsRequired)
        {
            currentHits =0;
            HoldBall(ball);
            canTrigger = false;
            StartCoroutine(SpawnBalls());
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

        if(objectName == "CampFire")
        {
            EventManager.InvokePlayAnimation("Fire");
            EventManager.InvokeRedLights();
            EventManager.InvokeStartBloodlust();
            StartCoroutine(BloodLustMode());
        }
        else if (objectName == "Cave")
        {
            EventManager.InvokePlayAnimation("Boulder");
        }

        canTrigger = false;

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

    IEnumerator SpawnBalls()
    {
        yield return new WaitForSeconds(holdDuration);
        ReleaseBall();

        for (int i = 0; i < ballsToSpawn; i++)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);
            EventManager.InvokeSpawnBallCave();
        }

        StartCoroutine(TriggerDelay());
    }

    IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(canTriggerDelay);
        canTrigger = true;
    }

    IEnumerator BloodLustMode()
    {
        yield return new WaitForSeconds(animationDelay);
        EventManager.InvokeStopBloodlust();
        EventManager.InvokeNormalLights();
    }

}
