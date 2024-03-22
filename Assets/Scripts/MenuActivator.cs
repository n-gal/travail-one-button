using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuActivator : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject menu;
   
    
    
    void Start()
    {
        
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
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
        menu.SetActive(true);
        isGamePaused = true;

    }

}
