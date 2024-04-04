using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSoundManager : MonoBehaviour
{
    public GameObject player;
    public float soundLerpSpeed = 5f;

    private Player playerScript;
    private Rigidbody2D playerRb;
    private AudioSource source;
    private float targetPitch;
    private float targetVolume;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        source = this.GetComponent<AudioSource>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }
    
    void UpdateSound()
    {

        if (Mathf.Abs(playerRb.velocity.x) + Mathf.Abs(playerRb.velocity.y) / 2 > 40f && !playerScript.isDead)
        {
            float playerVelocity = (Mathf.Abs(playerRb.velocity.x) + Mathf.Abs(playerRb.velocity.y)) / 120;
            targetPitch = Mathf.Clamp(playerVelocity, 0.7f, 2f);
            targetVolume = Mathf.Clamp(playerVelocity, 0f, 1f) / 3;

        }
        else
        {
            targetVolume = 0f;
        }
        
        targetPitch = Mathf.Lerp(source.pitch, targetPitch, soundLerpSpeed * Time.deltaTime);
        targetVolume = Mathf.Lerp(source.volume, targetVolume, soundLerpSpeed * Time.deltaTime);

        source.pitch = targetPitch;
        source.volume = targetVolume;
    }
}
