using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

    [Header("Light sources")]
    [SerializeField] GameObject spotLight;

    [Header("Text GameObjects")]
    [SerializeField] GameObject bloodlust;
    [SerializeField] GameObject launch;
    [SerializeField] GameObject tilt;


    void OnEnable()
    {
        EventManager.pauseMenu += PauseMenu;
        EventManager.showUIText += Showtext;
    }

    void OnDisable()
    {
        EventManager.pauseMenu -= PauseMenu;
        EventManager.showUIText -= Showtext;
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
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void ResumeGame()
    {
        spotLight.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Menu()
    {
        GameManager.Instance.ResetGame();
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        spotLight.SetActive(false);
        EventSystem.current.SetSelectedGameObject(startButton);
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

}
