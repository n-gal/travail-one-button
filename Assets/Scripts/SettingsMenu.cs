using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public Toggle autoRestartToggle;
    private string autoRestartString = "AutoRestart";
    public bool toggleValue;
    private void Start()
    {
        //autoRestartToggle.isOn = PlayerPrefs.GetInt(autoRestartString, 0) == 1;
        //autoRestartToggle.onValueChanged.AddListener(SaveToggleValue);
        toggleValue = PlayerPrefs.GetInt(autoRestartString, 0)==1;
        autoRestartToggle.isOn = toggleValue;
        autoRestartToggle.onValueChanged.AddListener(SaveToggleValue);

    }
    void SaveToggleValue(bool value)
    {
        PlayerPrefs.SetInt(autoRestartString, value ? 1 : 0);
        PlayerPrefs.Save();
    }
    
}
