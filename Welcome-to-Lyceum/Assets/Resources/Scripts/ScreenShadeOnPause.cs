using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShadeOnPause : MonoBehaviour
{
    public void Active()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
