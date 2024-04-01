using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraSmoothFollow : MonoBehaviour
{
    public Volume GlobalVolume;
    public Transform followTarget;
    public float zOffset;
    public float smoothness = 0.5f;
    public float cameraSizeSmoothness = 0.5f;
    public float chromaticStrength = 500f;
    public GameObject player;
    private Player playerScript;
    private Rigidbody2D playerRB;
    private Camera cameraC;
    private Vector3 targetPosition;
    private Vector3 relativePositionToTarget;
    private float defaultProjectionSize;
    private float targetOrthographicSize;
    private float targetChromaticStrength;

    private ChromaticAberration volumeChromaticAberration;

    void Start()
    {
        cameraC = this.GetComponent<Camera>();
        playerRB = player.GetComponent<Rigidbody2D>();
        relativePositionToTarget = transform.position - followTarget.position;
        targetPosition = followTarget.position + relativePositionToTarget;
        defaultProjectionSize = cameraC.orthographicSize;
        playerScript = player.GetComponent<Player>();

        if (GlobalVolume != null && GlobalVolume.profile.TryGet(out volumeChromaticAberration))
        {

        }
        else
        {
            Debug.LogWarning("Chromatic Aberration effect not found.");
        }
    }

    void Update()
    {
        targetPosition = followTarget.position + relativePositionToTarget;
        if(Mathf.Abs(playerRB.velocity.x) < 35)
        {
            targetOrthographicSize = defaultProjectionSize + (Mathf.Abs(playerRB.velocity.x) / 4 + Mathf.Abs(playerRB.velocity.y) / 6);
            targetChromaticStrength = (0f);
        }
        else
        {
            targetChromaticStrength = ((Mathf.Abs(playerRB.velocity.x) + Mathf.Abs(playerRB.velocity.y)) / chromaticStrength);
            targetOrthographicSize = defaultProjectionSize + (Mathf.Abs(playerRB.velocity.x) / 3 + Mathf.Abs(playerRB.velocity.y) / 4);
        }
        if(playerScript.isDead)
        {
            targetChromaticStrength = 0f;
            targetOrthographicSize = defaultProjectionSize;
        }
        volumeChromaticAberration.intensity.value = targetChromaticStrength;
        cameraC.orthographicSize = Mathf.Lerp(cameraC.orthographicSize, targetOrthographicSize, cameraSizeSmoothness * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, followTarget.position.z + zOffset);
    }
}
