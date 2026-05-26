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
        //TODO: subscribe to event
    }

    void OnDisable()
    {
        //TODO: unsubscribe to event
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
