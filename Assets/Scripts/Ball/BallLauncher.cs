using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] Ball currentBall;
    [SerializeField] Collider wallshootBlocker;

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
        if (currentBall == null) return;

        canShoot = false;
        wallshootBlocker.isTrigger = true;
        StartCoroutine(EnableWall());
        AudioManager.Instance.PlaySound("BumperClickClack");
        currentBall.Launch();
        EventManager.InvokeShowUIText(false, "LaunchText");
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

    IEnumerator EnableWall()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("WAll triggered");
        wallshootBlocker.isTrigger = false;
    }
}