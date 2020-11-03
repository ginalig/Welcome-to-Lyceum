using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PartIconColor : MonoBehaviour
{

    public List<SpriteRenderer> SpriteRenderers;
    public Image Icon;

    public List<Sprite> Icons;
    
    public int i = 0;
    
    void Update()
    {
        Icon.color = SpriteRenderers[i].color;
    }
}
