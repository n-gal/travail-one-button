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
    void Start()
    {
        progressBar = GetComponent<Image>();
        maxDistance = FinishLine.transform.position.x;
        progressBar.fillAmount = Player.transform.position.x / maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount = Player.transform.position.x / maxDistance;
        }
    }
}
