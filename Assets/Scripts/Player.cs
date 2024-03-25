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
    public GameObject deathParticle;
    public GameObject webTarget;
    public Gradient speedUpColour;
    public Transform webRayTransform;
    public float newHingeSpeed = -500f;
    public float newHingeTorque = 1500f;

    private float oldHingeSpeed;
    private float oldHingeTorque;
    private BoxCollider2D playerCollider;
    private HingeJoint2D webHinge;
    private bool webIsActive;
    private Vector3 raycastMemory;
    private bool isAttached;
    private bool isDead = false;
    private Rigidbody2D playerRigidBody;
    private TrailRenderer playerTrail;
    private Gradient oldTrailColour;



    void Start()
    {
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
    void Update()
    {
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
                        webHinge.enabled = true;
                        webHinge.connectedAnchor = hit.point;
                        webHinge.anchor = transform.InverseTransformPoint(hit.point);
                        Instantiate(attachmentParticle, hit.point, Quaternion.identity);
                    }
                    else if (hit.collider != null)
                    {
                        print("GRAHH");
                        webHinge.enabled = true;
                        webHinge.connectedAnchor = hit.point;
                        webHinge.anchor = transform.InverseTransformPoint(hit.point);
                        Instantiate(attachmentParticle, hit.point, Quaternion.identity);

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
            // Draw debug ray
            //Debug.DrawRay(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up) * 10f, Color.green);
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
                }
                Ray ray = new Ray(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up));

                int targetLayerMask = 1 << LayerMask.NameToLayer("WebWall");
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, targetLayerMask);

                if (hit.collider != null)
                {
                    webTarget.transform.position = new Vector3(hit.point.x, hit.point.y, -3);
                    raycastMemory = new Vector3(hit.point.x, hit.point.y, -3);
                }
                return;
            }
            isAttached = true;
            webTarget.transform.position = raycastMemory;
            return;
        }

        webTarget.SetActive(false);
    }
    IEnumerator DeathSequence()
    {
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

        //playerCollider.size = new Vector2(1, 1);
        Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        webHinge.enabled = false;
        isDead = true;
        Instantiate(detachmentParticle, raycastMemory, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


