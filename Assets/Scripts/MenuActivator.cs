using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuActivator : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject menu;
    public GameObject Deathscreen;
    public GameObject player;


    public Toggle autoRestartToggle;
    private string autoRestartString = "AutoRestart";
    public bool toggleValue;
    private void Start()
    {
        //autoRestartToggle.isOn = PlayerPrefs.GetInt(autoRestartString, 0) == 1;
        //autoRestartToggle.onValueChanged.AddListener(SaveToggleValue);
        toggleValue = PlayerPrefs.GetInt(autoRestartString, 0) == 1;
        autoRestartToggle.isOn = toggleValue;
        autoRestartToggle.onValueChanged.AddListener(SaveToggleValue);

    }
    void SaveToggleValue(bool value)
    {
        PlayerPrefs.SetInt(autoRestartString, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape)&&player.active==true)
       {
            if (!isGamePaused)
            {
                Debug.Log("Pause");
                Time.timeScale = 0;
                isGamePaused = true;
                menu.SetActive(true);

                
            }
            else
            {
                Debug.Log("Resume");
                Time.timeScale = 1;
                isGamePaused = false;
                menu.SetActive(false);
            }
           
       }
       
    }
    public void isDead()
    {
        if (toggleValue == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            Deathscreen.SetActive(true);
            isGamePaused = true;
        }
        
        
        //player.SetActive(false);
       

    }


}
