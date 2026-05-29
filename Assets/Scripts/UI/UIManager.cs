using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject spotLight;

    bool pauseMenuActive = false;

    void Start()
    {
        
    }

    public void StartGame()
    {
        spotLight.SetActive(true);
        mainMenu.SetActive(false);
        GameManager.Instance.ResetGame();
        GameManager.Instance.StartNextBall();
    }

    void OnPauseMenu()
    {
        if (pauseMenuActive)
        {
            pauseMenuActive = false;
            spotLight.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenuActive = true;
            spotLight.SetActive(false);
            pauseMenu.SetActive(true);

        }
    }

}
