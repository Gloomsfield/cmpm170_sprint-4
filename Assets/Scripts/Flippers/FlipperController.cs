using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/* This class is accessing the InputSystem directly for testing/demonstration
 * purposes */
public class FlipperController : MonoBehaviour {
    private HingeJoint _hingeJoint;
    private JointMotor _jointMotor;
    private KeyControl _key;

    private enum Side { LEFT = 1, RIGHT = 2 };

    [Header("Motor Attributes")]
    [SerializeField] private float flipperVelocity = 500f;
    [SerializeField] private float maxFlipperAngle = 30f;
    [SerializeField] private Side side;

    private bool _buttonHeld = false;

    private bool _enabled = false;

    void Start() {
         //flipperOff = InputSystem.actions.FindAction("LeftPaddle");
         //flipperOff.canceled += (_) => Debug.Log("it was cancelled");
         //Debug.Log(flipperOff);
        _hingeJoint = GetComponent<HingeJoint>();
        _jointMotor = _hingeJoint.motor;

        SetMotorAttributes();
        SubscribeToTriggerEvent();

        EventManager.roundEnded += () => { _enabled = false; };
        EventManager.spawnBall += () => { _enabled = true; Debug.Log("enabled"); };
    }

    void Update() {
        if (!_enabled) return;
        if (_buttonHeld) return;
        if (!(_hingeJoint.angle >= maxFlipperAngle - 5)) return;

        _jointMotor.targetVelocity = -flipperVelocity;
        _hingeJoint.motor = _jointMotor;
    }

    private void EnableFlipper() {
        if (!_enabled) return;
        _buttonHeld = true;
        if (_hingeJoint.angle > 5) return;

        _jointMotor.targetVelocity = flipperVelocity;
        _jointMotor.force = flipperVelocity;
        _hingeJoint.motor = _jointMotor;
    }

    private void SetMotorAttributes() {
        _hingeJoint.useMotor = true;
        _hingeJoint.useLimits = true;

        var hingeLimits = _hingeJoint.limits;
        hingeLimits.max = maxFlipperAngle;
        _hingeJoint.limits = hingeLimits;
    }

    private void SubscribeToTriggerEvent() {
        if (side == 0) {
            throw new ArgumentNullException("Paddle side not set");
        }

        var actionMap = InputSystem.actions;
        InputAction flipperAction;
        if (side == Side.LEFT) {
            flipperAction = actionMap.FindAction("LeftPaddle");
            flipperAction.started +=  (_) => EnableFlipper();
            //EventManager.leftFlipperTriggered += EnableFlipper;
            
        } else {
            flipperAction = actionMap.FindAction("RightPaddle");
            flipperAction.started += (_) => EnableFlipper();
            //EventManager.rightFlipperTriggered += EnableFlipper;
        }
        flipperAction.canceled += (_) => _buttonHeld = false; 
    }
}
