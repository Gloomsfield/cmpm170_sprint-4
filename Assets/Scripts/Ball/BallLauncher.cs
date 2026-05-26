using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] Ball currentBall;

    bool canShoot = false;

    void OnEnable()
    {
        EventManager.shootBall += TryShootBall;
    }

    void OnDisable()
    {
        EventManager.shootBall -= TryShootBall;
    }

    void TryShootBall()
    {
        if (!canShoot) return;
        if (currentBall != null) return;

        canShoot = false;
        currentBall.Launch();
    }

    public void SetBall(Ball ball)
    {
        currentBall = ball;
        canShoot = true;
    }

    public void ClearBall()
    {
        currentBall = null;
        canShoot = false;
    }
}