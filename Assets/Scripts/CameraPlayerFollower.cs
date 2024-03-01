using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
