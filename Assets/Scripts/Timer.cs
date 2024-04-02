using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public float bestTime=100;// = Mathf.Infinity;
    public TMP_Text highScore;
    public float currentTime = 0;
    void Start()
    {
        timeIsRunning = true;
        bestTime = PlayerPrefs.GetFloat("BestTime");
        highScore.text = bestTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining>=0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
        //if (!timeIsRunning) 
        //{
        //    Debug.Log("Stop!");
        //    currentTime = Time.deltaTime; 
        //}
    }
    
    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float miliseconds = Mathf.FloorToInt((timeToDisplay-(seconds + minutes*60))/100);
        float fraction = timeToDisplay * 1000;
        fraction = fraction % 1000;
        timeText.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }

    public void playerAsWon()
    {
        timeIsRunning=false;
        currentTime = Time.deltaTime;
        Debug.Log("player as won !");
        if (currentTime < bestTime)
        {
            Debug.Log("new record !");
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save(); 
            
        }
    }
}
