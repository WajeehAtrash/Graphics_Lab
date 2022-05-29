using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;//locking the cursor into the middle of the screen
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;//locking the cursor into the middle of the screen
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Next()
    {
        int nextLevelIndx = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndx < SceneManager.sceneCount)
        {
            SceneManager.LoadScene(nextLevelIndx);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Prev()
    {
        int prevLevelIndx = SceneManager.GetActiveScene().buildIndex + 1;
        if (prevLevelIndx > 0)
        {
            SceneManager.LoadScene(prevLevelIndx);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
