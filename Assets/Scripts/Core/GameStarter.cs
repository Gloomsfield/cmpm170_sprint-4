using UnityEngine;


// TOREMOVE This class is so we can call GameManager startNextBall which will be moved to some call when we have a main menu screen.
public class GameStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.ResetGame();
        GameManager.Instance.StartNextBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
