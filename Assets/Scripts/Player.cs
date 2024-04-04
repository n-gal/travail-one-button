using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Burst.CompilerServices;

public class Player : MonoBehaviour
{
    public GameObject playerVisual;
    public GameObject playerSlice1;
    public GameObject playerSlice2;
    public GameObject attachmentParticle;
    public GameObject detachmentParticle;
    public GameObject portalParticle;
    public GameObject deathParticle;
    public GameObject webTarget;
    public Gradient speedUpColour;
    public Transform webRayTransform;
    public float newHingeSpeed = -500f;
    public float newHingeTorque = 1500f;
    public GameObject webLineObject;
    public float webLineSpeed = 50f;
    public SoundManager2D soundManager;
    public GameObject escapeMenuManager;
    public AudioClip webAudio;
    public AudioClip webDisconnectAudio;
    public AudioClip webDeathAudio;

    private LineRenderer webLine;
    private bool canSeeWall;
    private float oldHingeSpeed;
    private float oldHingeTorque;
    private BoxCollider2D playerCollider;
    private HingeJoint2D webHinge;
    private bool webIsActive;
    private Vector3 raycastMemory;
    private bool isAttached;
    private Rigidbody2D playerRigidBody;
    private TrailRenderer playerTrail;
    private Gradient oldTrailColour;
    private MenuActivator menuActivator;
    private bool webLineIsWindingUp;

    [HideInInspector]
    public bool isDead = false;



    void Awake()
    {
        webLine = webLineObject.GetComponent<LineRenderer>();
        menuActivator = escapeMenuManager.GetComponent<MenuActivator>();
        playerTrail = playerVisual.GetComponent<TrailRenderer>();
        playerRigidBody = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();
        webHinge = GetComponent<HingeJoint2D>();
        oldHingeSpeed = webHinge.motor.motorSpeed;
        oldHingeTorque = webHinge.motor.maxMotorTorque;
        oldTrailColour = playerTrail.colorGradient;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isDead)
        {
            StartCoroutine(DeathSequence());
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(WinSequence());
    }

    void Update()
    {
        UpdateWeb();
        DrawTarget();
        if (!isDead)
        {

            if (Input.GetKeyDown("space"))
            {
                if (!webIsActive)
                {
                    Ray ray = new Ray(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up));
                    int targetLayerMask = 1 << LayerMask.NameToLayer("WebWall");
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, targetLayerMask);

                    if (hit.collider != null && !hit.collider.CompareTag("SpeedUpWall"))
                    {
                        ConnectWebHinge(hit);
                    }
                    else if (hit.collider != null)
                    {
                        ConnectWebHinge(hit);

                        JointMotor2D newMotor = webHinge.motor;
                        newMotor.motorSpeed = newHingeSpeed;
                        newMotor.maxMotorTorque = newHingeTorque;

                        webHinge.motor = newMotor;
                        playerTrail.colorGradient = speedUpColour;
                    }
                    webIsActive = true;
                }
                else
                {


                }
            }
            if (Input.GetKeyUp("space"))
            {
                webHinge.enabled = false;
                webIsActive = false;
                JointMotor2D newMotor = webHinge.motor;
                newMotor.motorSpeed = oldHingeSpeed;
                newMotor.maxMotorTorque = oldHingeTorque;

                webHinge.motor = newMotor;

                playerTrail.colorGradient = oldTrailColour;
            }
        }
    }
    void DrawTarget()
    {
        if(!isDead)
        {
            if (!webIsActive)
            {
                if (isAttached)
                {
                    isAttached = false;
                    Instantiate(detachmentParticle, raycastMemory, Quaternion.identity);
                    soundManager.PlaySound(webDisconnectAudio);
                }

                Ray ray = new Ray(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up));
                int targetLayerMask = 1 << LayerMask.NameToLayer("WebWall");
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, targetLayerMask);

                if (hit.collider != null)
                {
                    webTarget.transform.position = new Vector3(hit.point.x, hit.point.y, -3);
                    raycastMemory = new Vector3(hit.point.x, hit.point.y, -3);
                    canSeeWall = true;
                    return;
                }
                else
                {
                    canSeeWall = false;
                }
            }
            if(canSeeWall)
            {
                isAttached = true;
                webTarget.transform.position = raycastMemory;
                return;
            }
        }
        webTarget.SetActive(false);
    }
    IEnumerator DeathSequence()
    {
        soundManager.PlaySound(webDeathAudio);
        isAttached = false;
        Rigidbody2D Slice1RB = playerSlice1.GetComponent<Rigidbody2D>();
        Rigidbody2D Slice2RB = playerSlice2.GetComponent<Rigidbody2D>();
        playerRigidBody.simulated = false;
        playerVisual.SetActive(false);
        playerSlice1.SetActive(true);
        playerSlice2.SetActive(true);
        Slice1RB.AddForce(playerRigidBody.velocity * 50);
        Slice2RB.AddForce(playerRigidBody.velocity * 50);
        Slice1RB.MoveRotation(Random.Range(0f, 2000f));
        Slice2RB.MoveRotation(Random.Range(0f,2000f));
        Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        webHinge.enabled = false;
        isDead = true;
        Instantiate(detachmentParticle, raycastMemory, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        menuActivator.isDead();
    }

    void ConnectWebHinge(RaycastHit2D hit)
    {
        webLineIsWindingUp = true;
        webHinge.enabled = true;
        webHinge.connectedAnchor = hit.point;
        webHinge.anchor = transform.InverseTransformPoint(hit.point);
        Instantiate(attachmentParticle, hit.point, Quaternion.identity);

    }

    IEnumerator WinSequence()
    {
        Quaternion originalRotation = Quaternion.Euler(0f, -90f, 90f);
        Instantiate(portalParticle, this.transform.position, originalRotation);
        playerRigidBody.AddForce(new Vector2(50000, 0));
        yield return new WaitForSeconds(1f);
        playerRigidBody.drag = 2;
        yield return new WaitForSeconds(2f);
        playerRigidBody.simulated = false;
    }

    void UpdateWeb()
    {
        if(webLineIsWindingUp)
        {
            webLine.SetPosition(1, playerVisual.transform.position);
            webLineIsWindingUp = false;
            soundManager.PlaySound(webAudio);
        }
        if(isAttached)
        {
            webLineObject.SetActive(true);
            SetWebPosition(playerVisual.transform.position, raycastMemory);
        }
        else if(webLineObject.activeSelf)
        {
            DetachWeb(playerVisual.transform.position);
        }
    }

    void SetWebPosition(Vector3 playerPos, Vector3 webPos)
    {
        float playerTargetDistance = 1 / Vector3.Distance(playerPos, webPos) * 5;
        playerTargetDistance = Mathf.Clamp(playerTargetDistance, 0.1f, 0.35f);
        webLine.widthMultiplier = playerTargetDistance;
        webLine.positionCount = 2;
        webLine.SetPosition(0, playerPos);
        Vector3 targetPos = webPos;
        targetPos = Vector3.Lerp(webLine.GetPosition(1), targetPos, webLineSpeed * Time.deltaTime);
        webLine.SetPosition(1, targetPos);
    }
    void DetachWeb(Vector3 playerPos)
    {
        webLine.positionCount = 2;
        webLine.SetPosition(0, playerPos);

        Vector3 targetPos = Vector3.Lerp(webLine.GetPosition(1), playerPos, webLineSpeed * Time.deltaTime);
        webLine.SetPosition(1, targetPos);
        if(webLine.GetPosition(1) == playerPos)
        {
            webLineObject.SetActive(false);
        }
    }
}


