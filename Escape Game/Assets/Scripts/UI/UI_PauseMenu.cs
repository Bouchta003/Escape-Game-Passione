using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    //Locate the pause menu
    [SerializeField] GameObject pauseMenu;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void ButtonLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf == false)
            Time.timeScale = 0;
        else Time.timeScale = 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
