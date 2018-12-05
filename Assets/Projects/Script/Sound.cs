﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    [Range(0f,1f)]
    public float Volume;
    [Range(0.3f, 3f)]
    public float Pitch;

    [HideInInspector]
    public AudioSource Source;

}
