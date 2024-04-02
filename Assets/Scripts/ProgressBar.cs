using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject FinishLine;

    Image progressBar;
    float maxDistance;
    public GameObject winingScreen;

    public GameObject escapeMenuManager;
    private MenuActivator menuActivator;
    void Start()
    {
        progressBar = GetComponent<Image>();
        maxDistance = FinishLine.transform.position.x;
        progressBar.fillAmount = Player.transform.position.x / maxDistance;
        menuActivator = escapeMenuManager.GetComponent<MenuActivator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount = Player.transform.position.x / maxDistance;
        }

        if (progressBar.fillAmount == 1) 
        {
            //Time.timeScale = 0;
            Debug.Log("You Win !");
            //winingScreen.SetActive(true);
            menuActivator.asWon();
        }
    }
}
