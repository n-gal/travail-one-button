using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuActivator : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject menu;
    public GameObject Deathscreen;
    public GameObject player;
  
    
    
    void Start()
    {
        
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
        Deathscreen.SetActive(true);
        isGamePaused = true;
        
        
        //player.SetActive(false);
       

    }


}
