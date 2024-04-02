using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public float timeStart;
    public TMP_Text textBox;
    bool timerActive = true;
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive==true)
        {
            timeStart += Time.deltaTime;
            textBox.text = timeStart.ToString("F2");
        }
       
    }

    public void PlayerHasWon() 
    {
        timerActive = false;
    }

}
