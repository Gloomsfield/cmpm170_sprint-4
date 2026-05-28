public class ScoreManager {
    
	private uint _score = 0;
	private float _scoreMultiplier = 1.0f;

	private ScoreManager _instance;
	public ScoreManager Instance {
		get {
			if(_instance == null) {
				_instance = new();
				
				EventManager.increaseScore += IncreaseScore;
			}

			return _instance;
		}
	}

	private void IncreaseScore(uint delta) {
		_score += delta;

		EventManager.InvokeScoreUpdated(_score);
	}
	
}
