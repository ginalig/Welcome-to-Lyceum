using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pelmeniparticle : MonoBehaviour
{
    public UnityEvent onHit;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit!");
        var col = GetComponent<ParticleSystem>().collision;
        col.enabled = false;
        onHit.Invoke();
    }
}
