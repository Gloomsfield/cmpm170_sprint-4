using System;
using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private Rigidbody _rb;
    private float _lastFrameTip = 0;
    private bool _canTip = true;
    private bool _tipping = false;

    private enum Direction { NONE, LEFT, RIGHT }
    private Direction direction;

    private Coroutine _tipCoroutine;

    [Header("Ball Settings")]
    [SerializeField] private float shootForce = 10.0f;
    [SerializeField] private float ballTippingTime = 0.1f;
    [SerializeField] private float forceMultiplier = 5f;


    void OnEnable() {
        //EventManager.reset += ResetBall;
        EventManager.rotation += PushBall;
        _rb = GetComponent<Rigidbody>();
    }

    void OnDestroy() {
        EventManager.rotation -= PushBall;
        //EventManager.reset -= ResetBall;
        StopAllCoroutines();
    }

    public void Launch() {
        Debug.Log("Ball shot!");
        _rb.AddForce(0, 0, -shootForce, ForceMode.Impulse);
    }


    /*void ResetBall() {
        Debug.Log("Ball reset!");
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 10, -3);
    }*/

    private void PushBall (float amount) {

        if (!GameManager.Instance.CanTilt) return;

        if (amount < -0.8 || amount > 0.8) { EventManager.InvokeRoundEnded(); 
            Debug.Log("OVERTURN");
        }
        if (amount > -0.1 && amount < 0.1) {
            _canTip = true;
            _lastFrameTip = 0;
            direction = Direction.NONE;
            return;
        }
        if (!_canTip) {
            return;
        }

        if (_tipping && BallChangedDirection(direction, amount)) {
            //Debug.Log("WENT OTHER WAY");
            StopCoroutine(_tipCoroutine);
            _rb.linearVelocity = Vector3.zero;
            _canTip = false;
            _tipping = false;
            return;
        }

        if (!_tipping) {
            _tipCoroutine = StartCoroutine(TipBall());
            direction = amount < 0 ? Direction.LEFT : Direction.RIGHT;
        }
        // Debug.Log($"Receiving force {amount}, last tip {_lastFrameTip}");
        var tipAmount = amount - _lastFrameTip;
        _lastFrameTip += tipAmount;
        var forceVector = new Vector3(tipAmount * forceMultiplier, 0, 0);
        //var newPosition = transform.position + forceVector; 
        _rb.linearVelocity += forceVector;
        //Debug.Log($"Applying force {tipAmount}");
    }

    private bool BallChangedDirection(Direction currentDirection, float forceAmount) {
        if (currentDirection == Direction.NONE) {
            return forceAmount != 0;
        }

        var tipAmount = forceAmount - _lastFrameTip;
        if (currentDirection == Direction.LEFT) {
            return tipAmount > 0 ? true : false; 
        }
        return tipAmount < 0 ? true : false; 
    }

    private IEnumerator TipBall() {
        if (this == null) yield break;
        _tipping = true;
        yield return new WaitForSeconds(ballTippingTime);
        _rb.linearVelocity = Vector3.zero;
        _canTip = false;
        _tipping = false;
        //GameManager.Instance.DisableTilt();
        EventManager.InvokeFlippersDisabled();
        EventManager.InvokeShowUIText(true, "TiltText");
        EventManager.InvokePlayReflectionAnimation("BoarFaceAngry");
    }
}
