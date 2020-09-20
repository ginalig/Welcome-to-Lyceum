using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletedNotification : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    
    public void GetDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(GetDisabledAfterSeconds(3f));
        _animator.SetTrigger("QuestCompleted");
    }

    private IEnumerator GetDisabledAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
