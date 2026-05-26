using UnityEngine;

public class BallDrain : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();

        if (ball == null) return;
        
        Destroy(ball.gameObject);
        Debug.Log("Ball drained!");

        GameManager.Instance.BallDrained();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
