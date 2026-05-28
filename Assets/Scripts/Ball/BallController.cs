using UnityEngine;

public class BallController : MonoBehaviour {

    Rigidbody rb;

    [SerializeField] private float forceMultiplier = 25f;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        EventManager.machineTilted += PushBall;
        //EventManager.rotationAD += PushBall;
    }

    private void PushBall (float amount) {
        Debug.Log($"Applying force {amount}");
        Vector3 forceVector = new Vector3(amount * forceMultiplier * 50, 0, 0);
        rb.AddForce(forceVector);
    }

}
