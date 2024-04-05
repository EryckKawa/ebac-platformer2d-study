using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string floatParam = "MyExposedParam";

    public void ChangeValue(float f)
    {
        audioMixer.SetFloat(floatParam, f);
    }
}
