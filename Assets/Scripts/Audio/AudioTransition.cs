using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTransition : MonoBehaviour
{
    public AudioMixerSnapshot audioMixerSnapshot;
    public float transitionTime = 1;

    public void MakeTransition()
    {
        audioMixerSnapshot.TransitionTo(transitionTime);
    }
}
