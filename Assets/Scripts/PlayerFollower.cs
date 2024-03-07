using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform target;
    public float zOffset;
    private Vector3 relativePositionToTarget;

    private void Start()
    {
        relativePositionToTarget = transform.position - target.position;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x + relativePositionToTarget.x, target.position.y + relativePositionToTarget.y, zOffset);
    }
}