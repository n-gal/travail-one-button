using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject particlePrefab;
    public Transform webRayTransform;
    private HingeJoint2D webHinge;
    private bool webIsActive;
    private bool webIsConnected;
    private MenuActivator menuActivator;
    public GameObject escapeMenuManager;
    

    void Start()
    {
        webHinge = GetComponent<HingeJoint2D>();
        menuActivator = escapeMenuManager.GetComponent<MenuActivator>();


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PlayerDeathSequence());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (!webIsActive)
            {
                Ray ray = new Ray(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up));

                int targetLayerMask = 1 << LayerMask.NameToLayer("WebWall");
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, targetLayerMask);

                if (hit.collider != null)
                {
                    webHinge.enabled = true;
                    webHinge.connectedAnchor = hit.point;
                    webHinge.anchor = transform.InverseTransformPoint(hit.point);
                    Instantiate(particlePrefab, hit.point, Quaternion.identity);
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
        }
        // Draw debug ray
        Debug.DrawRay(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up) * 10f, Color.green);
    }
    Vector3 AnchorWorldPosition()
    {
        return transform.TransformPoint(webHinge.anchor);
    }
    IEnumerator PlayerDeathSequence()
    {
        yield return new WaitForSeconds(1f);
        menuActivator.isDead();
    }
}


