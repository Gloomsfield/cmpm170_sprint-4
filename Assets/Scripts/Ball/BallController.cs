using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour {

    private Rigidbody _rb;

    private bool _rotationReceivedLastFrame = false;
    private const int _tiltStopFrameThreshold = 5;
    private int _framesSinceLastRotation = 0;
    private float _lastFrameTip = 0;

    private bool _tipping = false;
    private bool _canTip = true;

    [SerializeField] private float forceMultiplier = 100f;
    [SerializeField] private float startThreshold = 0.1f;

    private void Start() {
        _rb = GetComponent<Rigidbody>();

       // _rb.AddForce(new Vector3(100, 0, 0));
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
        //_rotationReceivedLastFrame = true;
        //if (amount < startThreshold) return;
        Debug.Log($"Receiving force {amount}, last tip {_lastFrameTip}");
        var tipAmount = amount - _lastFrameTip;
        _lastFrameTip += tipAmount;
        //if (amount < ) return;
        Vector3 forceVector = new Vector3(tipAmount * forceMultiplier * 20, 0, 0);
        _rb.AddForce(forceVector);
        Debug.Log($"Applying force {tipAmount}");
    }

    /*
    void Update() {
        UpdateFrameRotationData();
        if (_framesSinceLastRotation > _tiltStopFrameThreshold) {
            Debug.Log("NO LONGER MOVING");
        }
    }
    */

    private IEnumerator TipBall() {
        yield return new WaitForSeconds(0.2f);
        _canTip = false;
    }

    private void UpdateFrameRotationData() {
        if (_rotationReceivedLastFrame) {
            _framesSinceLastRotation = 0;
            return;
        }
        _framesSinceLastRotation++;
        _rotationReceivedLastFrame = false;
    }
}
