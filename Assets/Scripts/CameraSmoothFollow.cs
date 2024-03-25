using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform followTarget;
    public float zOffset;
    public float smoothness = 0.5f;
    public float cameraSizeSmoothness = 0.5f;
    public GameObject player;
    private Rigidbody2D playerRB;
    private Camera cameraC;
    private Vector3 targetPosition;
    private Vector3 relativePositionToTarget;
    private float defaultProjectionSize;

    void Start()
    {
        cameraC = this.GetComponent<Camera>();
        playerRB = player.GetComponent<Rigidbody2D>();
        relativePositionToTarget = transform.position - followTarget.position;
        targetPosition = followTarget.position + relativePositionToTarget;
        defaultProjectionSize = cameraC.orthographicSize;
    }

    void Update()
    {
        targetPosition = followTarget.position + relativePositionToTarget;
        float targetOrthographicSize = defaultProjectionSize + (playerRB.velocity.x / 2 + Mathf.Abs(playerRB.velocity.y) / 4);
        cameraC.orthographicSize = Mathf.Lerp(cameraC.orthographicSize, targetOrthographicSize, cameraSizeSmoothness * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, followTarget.position.z + zOffset);
    }
}
