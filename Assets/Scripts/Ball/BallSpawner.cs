using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] BallLauncher ballLauncher;

    [Header("Ball Prefab")]
    [SerializeField] GameObject ballPrefab;

    [Header("Ball Spawner Location")]
    [SerializeField] Transform spawnLocation;
    [Header("Ball Spawner Location Cave")]
    [SerializeField] Transform spawnLocationCave;


    void OnEnable()
    {
        EventManager.spawnBall += SpawnBall;
        EventManager.spawnBallCave += SpawnBallCave;
    }

    void OnDisable()
    {
        EventManager.spawnBall -= SpawnBall;
        EventManager.spawnBallCave -= SpawnBallCave;
    }

    void SpawnBall()
    {
        GameObject ballObject = Instantiate(ballPrefab, spawnLocation.position, spawnLocation.rotation);
        Ball ball = ballObject.GetComponent<Ball>();
        ballLauncher.SetBall(ball);
        GameManager.Instance.RegisterBall();
    }

    void SpawnBallCave()
    {
        Instantiate(ballPrefab, spawnLocationCave.position, spawnLocationCave.rotation);
    }

}
