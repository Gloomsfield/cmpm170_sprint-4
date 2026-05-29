using UnityEngine;

public class ScoreManager {
    
	private uint _score = 0;
	private float _scoreMultiplier = 1.0f;

	private static ScoreManager _instance;
	public static ScoreManager Instance {
		get {
			if(_instance == null) {
				_instance = new();
				
				EventManager.increaseScore += _instance.IncreaseScore;
			}

			return _instance;
		}
	}

	private void IncreaseScore(uint delta) {
		_score += delta;

		EventManager.InvokeScoreUpdated(_score);
	}
	
}
