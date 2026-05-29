using UnityEngine;

class Bumper : MonoBehaviour {

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Ball")) {
			EventManager.InvokeScoreIncreased(600);
			Vector3 delta = collision.gameObject.transform.position - gameObject.transform.position;
			delta.y = 0.0f;
			delta.Normalize();

			collision.gameObject.GetComponent<Rigidbody>().AddForce(
				delta * 800
			);

			AudioManager.Instance.PlaySound("Ping1");

			int randomInt = Random.Range(0, 10);
			if(randomInt == 1)
			{
				EventManager.InvokePlayReflectionAnimation("BoarFaceVoice");
			}
		}
	}

}
