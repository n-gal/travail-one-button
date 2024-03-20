using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLvl1()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayLvl2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void PlayLvl3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;


    }


}
