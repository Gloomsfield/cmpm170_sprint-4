using UnityEngine;

public class CamShakeController : MonoBehaviour {

    [SerializeField] private float maxOffset = 0.2f;

    void Start() {
        EventManager.rotation += TiltCam;
    }

    private void TiltCam(float shakeStrength) {
        Vector3 currentPos = gameObject.transform.position;
        float proposedTiltAmount = currentPos.x + shakeStrength * 0.05f;
       // float tiltAmount = Mathf.Clamp(proposedTiltAmount, -maxOffset, maxOffset);
        gameObject.transform.position = new Vector3(
                proposedTiltAmount,
                currentPos.y,
                currentPos.z
                );
    }

}
