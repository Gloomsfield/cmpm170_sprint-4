using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FlipperController : MonoBehaviour {
    private HingeJoint _hingeJoint;
    private JointMotor _jointMotor;
    private KeyControl _key;

    private enum Side { LEFT = 1, RIGHT = 2 };

    [Header("Motor Attributes")]
    [SerializeField] private float flipperVelocity = 500f;
    [SerializeField] private float maxFlipperAngle = 30f;
    [SerializeField] private Side side;

    void Start() {
        _hingeJoint = GetComponent<HingeJoint>();
        _jointMotor = _hingeJoint.motor;

        SetMotorAttributes();
        SubscribeToTriggerEvent();
    }

    void Update() {
        if (!(_hingeJoint.angle >= maxFlipperAngle - 5)) return;

        _jointMotor.targetVelocity = -flipperVelocity;
        _hingeJoint.motor = _jointMotor;
    }

    private void EnableFlipper() {
        if (_hingeJoint.angle > 0) return;

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

        if (side == Side.LEFT) {
            EventManager.leftFlipperTriggered += EnableFlipper;
        } else {
            EventManager.rightFlipperTriggered += EnableFlipper;
        }
    }
}
