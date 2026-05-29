using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private Rigidbody _rb;
    private float _lastFrameTip = 0;
    private bool _canTip = true;

    [Header("Ball Settings")]
    [SerializeField] private float shootForce = 10.0f;
    [SerializeField] private float ballTippingTime = 0.1f;
    [SerializeField] private float forceMultiplier = 100f;


    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        //EventManager.shootBall += ShootBall;
        //EventManager.rotation += GetRotation;
        //EventManager.rotationAD += GetRotationAD;
        EventManager.rotation += PushBall;
        EventManager.reset += ResetBall;
    }

    void OnDisable() {
        //EventManager.shootBall -= ShootBall;
        //EventManager.rotation -= GetRotation;
        //EventManager.rotationAD -= GetRotationAD;
        //EventManager.reset -= ResetBall;
        EventManager.rotation -= PushBall;
        EventManager.reset -= ResetBall;
    }

    public void Launch() {
        Debug.Log("Ball shot!");
        GetComponent<Rigidbody>().AddForce(0, 0, -shootForce, ForceMode.Impulse);
    }


    void ResetBall() {
        Debug.Log("Ball reset!");
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 6, 13);
    }

    private void PushBall (float amount) {
        if (amount == 0) {
            _canTip = true;
            return;
        }
        if (!_canTip) {
            Debug.Log("CANNOT TIP");
            return;
        }

        StartCoroutine(TipBall());
        Debug.Log($"Receiving force {amount}, last tip {_lastFrameTip}");
        var tipAmount = amount - _lastFrameTip;
        _lastFrameTip += tipAmount;
        Vector3 forceVector = transform.position + new Vector3(tipAmount * forceMultiplier, 0, 0);
        _rb.MovePosition(forceVector);
        //_rb.AddForce(forceVector);
        //Debug.Log($"Applying force {tipAmount}");
    }


    private IEnumerator TipBall() {
        yield return new WaitForSeconds(ballTippingTime);
        _canTip = false;
    }
}
