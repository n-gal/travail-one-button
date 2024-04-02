using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelsInfo : MonoBehaviour
{
    [SerializeField] TMP_Text lvl1timeTxt;
    [SerializeField] TMP_Text lvl2timeTxt;
    [SerializeField] TMP_Text lvl3timeTxt;
    [SerializeField] float lvl1BestTime;
    [SerializeField] float lvl2BestTime;
    [SerializeField] float lvl3BestTime;
    // Start is called before the first frame update
    void Start()
    {
        lvl1BestTime = PlayerPrefs.GetFloat("BestTime"+1);
        lvl1timeTxt.text = "Best time : " + lvl1BestTime.ToString("F2");

        lvl2BestTime = PlayerPrefs.GetFloat("BestTime" + 2);
        lvl2timeTxt.text = "Best time : " + lvl2BestTime.ToString("F2");

        lvl3BestTime = PlayerPrefs.GetFloat("BestTime" + 3);
        lvl3timeTxt.text = "Best time : " + lvl3BestTime.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
