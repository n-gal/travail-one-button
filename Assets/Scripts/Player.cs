using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public GameObject particlePrefab;
    public Transform webRayTransform;
    private HingeJoint2D webHinge;
    private bool webIsActive;
    private bool webIsConnected;
    private MenuActivator menuActivator;
    public GameObject escapeMenuManager;
    public GameObject playerVisual;
    public ParticleSystem deathEffect;
    public GameObject player;
    public bool isDead=false;

    void Start()
    {
        webHinge = GetComponent<HingeJoint2D>();
        menuActivator = escapeMenuManager.GetComponent<MenuActivator>();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        //deathEffect.Emit(1);
        menuActivator.isDead();
        player.SetActive(false);
        //StartCoroutine(PlayerDeathSequence());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        


    }

    //private void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        // Accès au transform du joueur
    //        Transform playerTransform = collision.transform;

    //        // Création des particules de mort à la position du joueur
    //        if (deathEffect != null)
    //        {
    //            Instantiate(deathEffect, playerTransform.position, Quaternion.identity);
    //        }

    //        // Destruction du joueur
    //        Destroy(collision.gameObject);
    //    }
    //}

    //private void Die() 
    //{
    //    isDead = true;
    //    Instantiate(deathEffect, transform.position, Quaternion.identity);
    //    gameObject.SetActive(false);
    //}
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


