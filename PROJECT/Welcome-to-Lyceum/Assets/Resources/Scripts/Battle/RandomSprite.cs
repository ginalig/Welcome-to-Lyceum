using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public List<Sprite> Sprites;

    private void Start()
    {
        SpriteRenderer.sprite = Sprites[UnityEngine.Random.Range(0, Sprites.Count)];
    }
}
