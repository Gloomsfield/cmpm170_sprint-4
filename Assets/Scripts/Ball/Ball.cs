using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private Rigidbody _rb;
    private float _lastFrameTip = 0;
    private bool _canTip = true;
    private bool _tipping = false;

    private enum Direction { NONE, LEFT, RIGHT }
    private Direction direction;

    [Header("Ball Settings")]
    [SerializeField] private float shootForce = 10.0f;
    [SerializeField] private float ballTippingTime = 0.1f;
    [SerializeField] private float forceMultiplier = 100f;


    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        EventManager.rotation += PushBall;
        EventManager.reset += ResetBall;
    }

    void OnDisable() {
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
            direction = Direction.NONE;
            return;
        }
        if (!_canTip) {
            return;
        }

        if (_tipping && BallChangedDirection(direction, amount)) {
            Debug.Log("WENT OTHER WAY");
            StopCoroutine(TipBall());
            _canTip = false;
            return;
        }
        if (!_tipping) {
            StartCoroutine(TipBall());
            direction = amount < 0 ? Direction.LEFT : Direction.RIGHT;
        }
        //Debug.Log($"Receiving force {amount}, last tip {_lastFrameTip}");
        var tipAmount = amount - _lastFrameTip;
        _lastFrameTip += tipAmount;
        Vector3 newPosition = transform.position + new Vector3(tipAmount * forceMultiplier, 0, 0);
        _rb.MovePosition(newPosition);
        //Debug.Log($"Applying force {tipAmount}");
    }

    private bool BallChangedDirection(Direction currentDirection, float forceAmount) {
        if (currentDirection == Direction.NONE) {
            return forceAmount != 0 ? true : false;
        }
        
        var tipAmount = forceAmount - _lastFrameTip;
        if (direction == Direction.LEFT) {
           return tipAmount > 0 ? true : false; 
        }
           return tipAmount < 0 ? true : false; 
    }

    private IEnumerator TipBall() {
        _tipping = true;
        yield return new WaitForSeconds(ballTippingTime);
        _canTip = false;
        _tipping = false;
    }
}
