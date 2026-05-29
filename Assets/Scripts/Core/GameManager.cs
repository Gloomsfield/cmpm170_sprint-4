using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum GameState
    {
        PREGAME,
        INGAME,
        ROUNDEND,
        ENDGAME,
    }

    public static GameManager Instance { get; private set; } = new GameManager();

    public GameState State { get; private set; }

    int ballsLeft;
    int activeBalls;
    //int score;
    const int startingBalls = 3;

    GameManager()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        ballsLeft = startingBalls;
        activeBalls = 0;
        ScoreManager.Instance.ResetScore();
        State = GameState.PREGAME;
    }

    public void StartNextBall()
    {
        if (ballsLeft <= 0)
        {
            State = GameState.ENDGAME;
            Debug.Log("Game Over!");
            return;
        }

        ballsLeft--;
        activeBalls = 0;
        State = GameState.INGAME;
        EventManager.InvokeFlickerLight();
        EventManager.InvokeSpawnBall();
    }

    public void RegisterBall()
    {
        activeBalls++;
        Debug.Log("Ball registered. Active balls: " + activeBalls);
    }

    public void BallDrained()
    {
        activeBalls--;
        EventManager.InvokePlayReflectionAnimation("BoarFaceAngry");   
        Debug.Log("Ball drained. Active balls: " + activeBalls);

        if (activeBalls == 0)
        {
            EventManager.InvokeShowUIText(false, "TiltText");
            EndRound();
        }
    }

    void EndRound()
    {
        EventManager.InvokeFlickerLight();
        EventManager.InvokeRoundEnded();
        State = GameState.ROUNDEND;

        if (ballsLeft > 0)
        {
            Debug.Log("Round ended. Starting next round.");
            StartNextBall();
        }
        else
        {
            State = GameState.ENDGAME;
            Debug.Log("Game Over!");
            if(ScoreManager.Instance.GetScore() >= 50000)
            {
                EventManager.InvokePlayReflectionAnimation("BoarFace");
            }
            else
            {
                EventManager.InvokePlayReflectionAnimation("BoarFaceAngry");    
            }

            EventManager.InvokeGameOver();
        }
    }

}

    /*
    Going to track:
    - score
    - balls/round left
    - game state
    - game over
    */
