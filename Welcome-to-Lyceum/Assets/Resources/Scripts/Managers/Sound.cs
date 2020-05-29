using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;

    [Range(.1f, 3f)] public float pitch;

    [HideInInspector] public AudioSource source;

    [Range(0f, 1f)] public float volume;
}