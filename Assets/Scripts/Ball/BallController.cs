using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour {

    [SerializeField] private float forceMultiplier = 100f;

    private Rigidbody _rb;

    private float _lastFrameTip = 0;
    private bool _canTip = true;


    private void Start() {
        _rb = GetComponent<Rigidbody>();

        EventManager.rotation += PushBall;
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
        Vector3 forceVector = new Vector3(tipAmount * forceMultiplier * 20, 0, 0);
        _rb.AddForce(forceVector);
        //Debug.Log($"Applying force {tipAmount}");
    }


    private IEnumerator TipBall() {
        yield return new WaitForSeconds(0.2f);
        _canTip = false;
    }
}
