using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Different Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject pauseMenu;

    [Header("Set Selected Buttons")]
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject creditButton;
    [SerializeField] GameObject menuButton2;

    [Header("Light sources")]
    [SerializeField] GameObject spotLight;

    [Header("Text GameObjects")]
    [SerializeField] GameObject bloodlust;
    [SerializeField] GameObject launch;
    [SerializeField] GameObject tilt;
    [SerializeField] GameObject gameOver;

    [Header("My brain doesnt know what name to make this")]
    [SerializeField] TextMeshProUGUI text;


    void OnEnable()
    {
        EventManager.pauseMenu += PauseMenu;
        EventManager.showUIText += Showtext;
        EventManager.gameOver += GameOver;
    }

    void OnDisable()
    {
        EventManager.pauseMenu -= PauseMenu;
        EventManager.showUIText -= Showtext;
        EventManager.gameOver -= GameOver;
    }

    public void StartGame()
    {
        spotLight.SetActive(true);
        mainMenu.SetActive(false);
        GameManager.Instance.ResetGame();
        GameManager.Instance.StartNextBall();
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuButton);
    }

    public void HideCredits()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
        EventSystem.current.SetSelectedGameObject(creditButton);
    }

    void PauseMenu()
    {
        spotLight.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void ResumeGame()
    {
        spotLight.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Showtext(bool show, string name)
    {
        if(name == "BloodLustText")
        {
            bloodlust.SetActive(show);
        }
        else if (name == "TiltText")
        {
            tilt.SetActive(show);
        }
        else if (name == "LaunchText")
        {
            launch.SetActive(show);
        }
    }

    public void GameOver()
    {
        uint score = ScoreManager.Instance.GetScore();
        text.text = "Game Over!\nScore: " + score;
        gameOver.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

}
