using UnityEngine;

public class FlipperController : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private JointSpring _jointSpring;

    [Header("Spring Attributes")]
    [SerializeField] private float spring = 50f;
    [SerializeField] private float targetPosition = 20;
    [SerializeField] private float damper = 0f;


    void Start() {
        _hingeJoint = GetComponent<HingeJoint>();
        _jointSpring = _hingeJoint.spring;
        SetSpringAttributes();
    }

    void Update() {
        /*
        motor.motorSpeed = Input.GetKey(KeyCode.F) ? flipMotorSpeed : idleMotorSpeed;
        hingeJoint.motor = motor;
        */
    }

    private void SetSpringAttributes() {
        _jointSpring.spring = spring;
        _jointSpring.targetPosition = targetPosition;
        _jointSpring.damper = damper;
    }
}
