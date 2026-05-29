using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action shootBall;
    public static void InvokeShootBall()
    {
        shootBall?.Invoke();
    }

    public static event Action leftFlipperTriggered;
    public static void InvokeLeftFlipperTriggered()
    {
        leftFlipperTriggered?.Invoke();
    }
    
    public static event Action rightFlipperTriggered;
    public static void InvokeRightFlipperTriggered()
    {
        rightFlipperTriggered?.Invoke();
    }

    public static event Action<float> rotationAD;
    public static void InvokeRotationAD(float rotationValue)
    {
        rotationAD?.Invoke(rotationValue);
    }

    public static event Action<float> rotation;
    public static void InvokeRotation(float rotationValue)
    {
        rotation?.Invoke(rotationValue);
    }

    public static event Action reset;
    public static void InvokeReset()
    {
        reset?.Invoke();
    }

    public static event Action spawnBall;
    public static void InvokeSpawnBall()
    {
        spawnBall?.Invoke();
    }

    public static event Action spawnBallCave;
    public static void InvokeSpawnBallCave()
    {
        spawnBallCave?.Invoke();
    }

    public static event Action<Ball> holdBall;
    public static void InvokeHoldBall(Ball ball)
    {
        holdBall?.Invoke(ball);
    }

	public static event Action<uint> increaseScore;
	public static void InvokeScoreIncreased(uint delta) {
		ScoreManager _ = ScoreManager.Instance;
		increaseScore?.Invoke(delta);
	}

	public static event Action<uint> updateScore;
	public static void InvokeScoreUpdated(uint newScore) {
		updateScore?.Invoke(newScore);
	}

	public static event Action startBloodlust;
	public static void InvokeStartBloodlust() {
		startBloodlust?.Invoke();
	}

	public static event Action stopBloodlust;
	public static void InvokeStopBloodlust() {
		stopBloodlust?.Invoke();
	}

    public static event Action<string> playAnimation;
    public static void InvokePlayAnimation(string animationName)
    {
        playAnimation?.Invoke(animationName);
    }

    public static event Action<string> playReflectionAnimation;
    public static void InvokePlayReflectionAnimation(string animationName)
    {
        playReflectionAnimation?.Invoke(animationName);
    }
    
    public static event Action pauseMenu;
    public static void InvokePauseMenu()
    {
        pauseMenu?.Invoke();
    }
    
    public static event Action roundEnded;
    public static void InvokeRoundEnded()
    {
        roundEnded?.Invoke();
    }

    public static event Action redLights;
    public static void InvokeRedLights()
    {
        redLights?.Invoke();
    }

    public static event Action normalLights;
    public static void InvokeNormalLights()
    {
        normalLights?.Invoke();
    }

    public static event Action<bool, string> showUIText;
    public static void InvokeShowUIText(bool show, string name)
    {
        showUIText?.Invoke(show, name);
    }

    public static event Action<bool> particle;
    public static void InvokeParticle(bool show)
    {
        particle?.Invoke(show);
    }

    public static event Action gameOver;
    public static void InvokeGameOver()
    {
        gameOver?.Invoke();
    }
}
