﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleItem : MonoBehaviour
{
    public ParticleSystem ps;

    public UnityEvent OnStartEvent;
    public UnityEvent OnFinishEvent;

    private void Start()
    {
        OnStart();
    }
    
    public void OnStart()
    {
        OnStartEvent.Invoke();
    }

    public void OnFinish()
    {
        OnFinishEvent.Invoke();
    }
    
    public void PSPlay()
    { 
        if (ps == null) return;
        ps.Play();
    }

    public void SelfDestroy()
    {
        OnFinish();
       if (ps != null) ps.transform.parent = null;
        Destroy(gameObject);
    }
}
