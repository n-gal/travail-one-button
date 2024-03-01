using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private HingeJoint2D webHinge;
    private bool hingeEnabled;

    // Start is called before the first frame update
    void Start()
    {
        webHinge = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("test");
            webHinge.enabled = true;
        }
        if (Input.GetKeyUp("space"))
        {
            webHinge.enabled = false;
        }
    }
}
