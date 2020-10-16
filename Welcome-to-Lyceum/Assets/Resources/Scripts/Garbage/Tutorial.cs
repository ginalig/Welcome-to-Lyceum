using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    private InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Start()
    {
        controls.Disable();
    }

    public void EnableControls()
    {
        controls.Enable();
    }
}
