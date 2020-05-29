using UnityEngine;

public class PlayerBoundries : MonoBehaviour
{
    [SerializeField] private Transform up = null;
    [SerializeField] private Transform down = null;
    [SerializeField] private Transform left = null;
    [SerializeField] private Transform right = null;
    
    private Vector2 pos;
    
    private void LateUpdate()
    {
        pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
        pos.y = Mathf.Clamp(pos.y, down.position.y, up.position.y);
        transform.position = pos;
    }
}