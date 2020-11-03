using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomizeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = Random.Range(slider.minValue, slider.maxValue);
    }
}
