using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OGETransition : MonoBehaviour
{

    public BoolAsset isReadyForOGE;
    public UnityEvent OnReadyForOGEEvent;

    public BoolAsset isAfterOGE;
    public UnityEvent OnAfterOGE;
    
    private void Start()
    {
        if (isReadyForOGE.GetValue()) OnReadyForOGEEvent.Invoke();
        if (isAfterOGE.GetValue()) OnAfterOGE.Invoke();
    }
}
