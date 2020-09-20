using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private Animator animator = null;
    public bool isPaused;

    private bool isQuestMenuOpened = false;
    
    
    public void Pause()
    {
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = !isPaused;
        if (isPaused) Player.Instance.controls.Disable();
        else Player.Instance.controls.Enable();
    }

    public void OpenQuestMenu()
    {
        if (isQuestMenuOpened)
        {
            animator.SetTrigger("CloseQuestMenu");
            isQuestMenuOpened = false;
        }
        else
        {
            animator.SetTrigger("OpenQuestMenu");
            isQuestMenuOpened = true;
        }
    }
    
}
