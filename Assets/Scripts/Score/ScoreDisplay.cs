using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplay : MonoBehaviour {
    
	private void Start() {
		EventManager.updateScore += UpdateScore;

		UpdateScore(0);
	}

	private void UpdateScore(uint newScore) {
		GetComponent<TextMeshProUGUI>().SetText($"{newScore:N0}");
	}

}
