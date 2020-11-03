using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGradient : MonoBehaviour
{
   public UIGradient gradient;

   public void ChangeGradientColor(float value)
   {
      gradient.m_color2 = Color.HSVToRGB(value, 1, 1);
   }
}
