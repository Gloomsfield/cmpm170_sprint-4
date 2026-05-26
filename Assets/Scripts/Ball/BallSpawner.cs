using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Ball Prefab")]
    [SerializeField] GameObject ballPrefab;

    [Header("Ball Spawner Location")]
    [SerializeField] Vector3 spawnLocation;
    [Header("Ball Spawner Location Cave")]
    [SerializeField] Vector3 spawnLocationCave;


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
        Instantiate(ballPrefab, spawnLocation, Quaternion.identity);
    }

    void SpawnBallCave()
    {
        Instantiate(ballPrefab, spawnLocationCave, Quaternion.identity);
    }

}
