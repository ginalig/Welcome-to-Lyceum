using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public CinemachineImpulseSource cam;

    public void Shake()
    {
        cam.GenerateImpulse();
    }
}
