using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class DontDestroyOnLoad<T> : MonoBehaviour where T : DontDestroyOnLoad<T>
{
    public static T instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = (T) this;    
        
        DontDestroyOnLoad(this);
    }
}
