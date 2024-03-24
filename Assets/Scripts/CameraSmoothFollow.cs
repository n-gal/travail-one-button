using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform followTarget;
    public float zOffset;
    public float smoothness = 0.5f; // Adjust this to control the smoothness of the interpolation
    private Vector3 targetPosition;
    private Vector3 relativePositionToTarget;

    void Start()
    {
        relativePositionToTarget = transform.position - followTarget.position;
        targetPosition = followTarget.position + relativePositionToTarget;
    }

    void Update()
    {
        // Compute the target position including the offset
        targetPosition = followTarget.position + relativePositionToTarget;

        // Interpolate towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, followTarget.position.z + zOffset);
    }
}
