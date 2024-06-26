using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField] int levelNumber;
    public float timeStart;
    public TMP_Text timerText;
    public TMP_Text bestTimeText;
    bool timerActive = true;
    public float bestTime =50f;
    public GameObject highScore;
    // Start is called before the first frame update
    void Start()
    {
       
            bestTime = PlayerPrefs.GetFloat("BestTime" + levelNumber, Mathf.Infinity);
            bestTimeText.text = "Best time : " + bestTime.ToString("F2") + " Sec";
            timerText.text = timeStart.ToString("F2");
       
    }

    // Update is called once per frame
    void Update()
    {
        if (bestTime == Mathf.Infinity) 
        {
            highScore.SetActive(false);
        }

        if (timerActive==true)
        {
            timeStart += Time.deltaTime;
            timerText.text = timeStart.ToString("F2");
        }
       
    }

    public void PlayerHasWon() 
    {
        timerActive = false;

        if(timeStart<bestTime)
        {
            bestTime = timeStart;

           
                PlayerPrefs.SetFloat("BestTime" + levelNumber, bestTime);
          
            
        }
    }

}
