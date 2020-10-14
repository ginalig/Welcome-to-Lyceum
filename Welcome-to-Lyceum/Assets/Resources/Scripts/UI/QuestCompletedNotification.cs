using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletedNotification : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetDestroyedAfterSeconds(3));
    }

    private IEnumerator GetDestroyedAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }
}
