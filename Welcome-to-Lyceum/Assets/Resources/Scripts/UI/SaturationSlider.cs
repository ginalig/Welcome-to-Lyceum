using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaturationSlider : MonoBehaviour
{
    public Image slider;

    public void ChangeColor(float value)
    {
        slider.color = Color.HSVToRGB(value, 1, 1);
    }
}
