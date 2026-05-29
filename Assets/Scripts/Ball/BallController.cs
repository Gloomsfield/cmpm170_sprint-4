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

    [SerializeField] private float forceMultiplier = 25f;
    [SerializeField] private float startThreshold = 0.1f;

    private void Start() {
        _rb = GetComponent<Rigidbody>();

        EventManager.rotation += PushBall;
    }

    private void PushBall (float amount) {
        if (amount == 0) {
            _canTip = true;
            return;
        }
        if (!_canTip) return;

        StartCoroutine(TipBall());
        //_rotationReceivedLastFrame = true;
        //if (amount < startThreshold) return;
        Debug.Log($"Receiving force {amount}, last tip {_lastFrameTip}");
        var tipAmount = amount - _lastFrameTip;
        _lastFrameTip += tipAmount;
        //if (amount < ) return;
        Vector3 forceVector = new Vector3(tipAmount * forceMultiplier * 15, 0, 0);
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
        yield return new WaitForSeconds(1);
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
