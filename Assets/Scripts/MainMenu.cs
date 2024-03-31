using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLvl1()
    {
        SceneManager.LoadScene("Level1");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }


}
