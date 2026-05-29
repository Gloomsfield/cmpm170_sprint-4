using UnityEngine;

class Bumper : MonoBehaviour {

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Ball")) {
			Vector3 delta = collision.gameObject.transform.position - gameObject.transform.position;
			delta.y = 0.0f;
			delta.Normalize();

			collision.gameObject.GetComponent<Rigidbody>().AddForce(
				delta * 800
			);
		}
	}

}
