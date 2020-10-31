using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightColor : MonoBehaviour
{
    public Light2D _light;
    private Color color;
    
    public void SetColor(string hexCode)
    {
        if (ColorUtility.TryParseHtmlString("#" + hexCode, out color))
        {
            _light.color = color;
        }
    }
    
}
