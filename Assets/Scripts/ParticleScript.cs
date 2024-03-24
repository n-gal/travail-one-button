using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public float timeUntilDeletion = 1f;
    void Start() // Hello world :)
    {
        StartCoroutine(DeathTimer()); // Goodbye world :(
    }

    void Update()
    {
        
    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timeUntilDeletion);
        Destroy(this.gameObject);
    }
}