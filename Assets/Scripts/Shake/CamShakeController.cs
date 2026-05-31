using UnityEngine;

public class CamShakeController : MonoBehaviour {

    [SerializeField] private float moveMultiplier = 1f;

    private Vector3 _initialPosition;

    void Start() {
        EventManager.rotation += TiltCam;
        _initialPosition = transform.position;
    }

    private void TiltCam(float shakeStrength) {
        float proposedTiltAmount = _initialPosition.x + shakeStrength * moveMultiplier;
       // float tiltAmount = Mathf.Clamp(proposedTiltAmount, -maxOffset, maxOffset);
        gameObject.transform.position = new Vector3(
                proposedTiltAmount,
                _initialPosition.y,
                _initialPosition.z
                );
    }

    void OnDestroy()
    {
        EventManager.rotation -= TiltCam;
    }

}
