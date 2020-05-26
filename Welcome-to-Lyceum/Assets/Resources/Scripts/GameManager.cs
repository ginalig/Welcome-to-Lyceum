using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerMovement playerMovement;
    
    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        playerMovement.enabled = !playerMovement.enabled;
    }
}
