using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperController : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private JointMotor _jointMotor;

    [Header("Motor Attributes")]
    [SerializeField] private float flipMotorSpeed = 200f;
    [SerializeField] private float idleMotorSpeed = 0f;
    [SerializeField] private float force = 50f;

    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _jointMotor = _hingeJoint.motor;
        SetMotorAttributes();
    }

    void Update()
    {
        _hingeJoint.useMotor = true;
        //_jointMotor.targetVelocity = Keyboard.current.fKey.isPressed ? flipMotorSpeed : idleMotorSpeed;
        if (Keyboard.current.fKey.wasPressedThisFrame) {
            Debug.Log("f key was pressed");
            _jointMotor.targetVelocity = 50;
        }
        _hingeJoint.motor = _jointMotor;
    }

    private void SetMotorAttributes()
    {
        _jointMotor.force = force;
        _jointMotor.targetVelocity = idleMotorSpeed;
        _jointMotor.freeSpin = false;
        _hingeJoint.motor = _jointMotor;
    }
}
