using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FlipperController : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private JointMotor _jointMotor;
    private KeyControl _key;


    [Header("Motor Attributes")]
    [SerializeField] private float flipperVelocity = 500f;
    [SerializeField] private float maxFlipperAngle = 30f;
    [SerializeField] private bool right = false;

    void Start() {
        _hingeJoint = GetComponent<HingeJoint>();
        _jointMotor = _hingeJoint.motor;
        _key = right ? Keyboard.current.jKey : Keyboard.current.fKey;
        SetMotorAttributes();
    }

    void Update() {
        if (_hingeJoint.angle >= maxFlipperAngle - 5) {
            _jointMotor.targetVelocity = -flipperVelocity;
        } else if (_hingeJoint.angle > 0) {
            return;
        } else {
            if (_key.wasPressedThisFrame) {
                //Debug.Log("d key was pressed");
                _jointMotor.targetVelocity = flipperVelocity;
                _jointMotor.force = flipperVelocity;
            }
        }
        _hingeJoint.motor = _jointMotor;
    }

    private void SetMotorAttributes() {
        _hingeJoint.useMotor = true;
        _hingeJoint.useLimits = true;
        var hingeLimits = _hingeJoint.limits;
        hingeLimits.max = maxFlipperAngle;
        _hingeJoint.limits = hingeLimits;
        //_hingeJoint.limits.max = maxFlipperAngle;
    }
}
