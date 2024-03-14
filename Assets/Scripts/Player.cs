using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform webRayTransform;
    private HingeJoint2D webHinge;
    private bool webIsActive;
    private bool webIsConnected;

    void Start()
    {
        webHinge = GetComponent<HingeJoint2D>();
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
                }
                webIsActive = true;
            }
            else
            {
                // Draw debug ray
                Debug.DrawRay(webRayTransform.position, webRayTransform.TransformDirection(Vector3.up) * 10f, Color.green);

            }
        }
        if (Input.GetKeyUp("space"))
        {
            webHinge.enabled = false;
            webIsActive = false;
        }
        print(webIsActive);
    }
    Vector3 AnchorWorldPosition()
    {
        return transform.TransformPoint(webHinge.anchor);
    }
}


