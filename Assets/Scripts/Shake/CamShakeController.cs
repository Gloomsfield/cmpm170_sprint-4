using UnityEngine;

public class CamShakeController : MonoBehaviour {

    [SerializeField] private float maxOffset = 40f;

    void Start() {
        EventManager.rotation += TiltCam;
    }

    private void TiltCam(float amount) {
        Debug.Log("tilt cam received" + amount);
        Vector3 offset = new Vector3(amount, 0, 0);
        gameObject.transform.position += offset;
    }

}
