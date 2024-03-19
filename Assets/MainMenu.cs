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
}
