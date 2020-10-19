using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressedEffect : MonoBehaviour
{
    private BackgroundMusic music;

    private void OnEnable()
    {
        music = BackgroundMusic.instance;
        music.AudioSource.pitch /= 2;
    }

    private void OnDisable()
    {
        music.AudioSource.pitch *= 2;
    }
}
