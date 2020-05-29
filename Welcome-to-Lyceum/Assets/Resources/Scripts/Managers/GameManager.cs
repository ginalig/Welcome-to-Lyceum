using Resources.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerMovement = null;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }
    
    public void Pause()
    {
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        playerMovement.enabled = !playerMovement.enabled;
    }

    public void EnableDisable(GameObject other)
    {
        other.SetActive(!other.activeSelf);
    }

}