using UnityEngine;

public class BallController : MonoBehaviour {

    Rigidbody rb;

    [SerializeField] private float forceMultiplier = 25f;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        EventManager.rotation += PushBall;
    }

    private void PushBall (float amount) {
        Debug.Log($"Applying force {amount}");
        Vector3 forceVector = new Vector3(amount * forceMultiplier, 0, 0);
        rb.AddForce(forceVector);
    }

}
