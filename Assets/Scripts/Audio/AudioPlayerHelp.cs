using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelp : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Play();
        }
    }

    public void Play()
    {
        audioSource.Play();
    }
}
