using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2D : MonoBehaviour
{
    private AudioSource source;

    void Awake()
    {
        source = this.GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
