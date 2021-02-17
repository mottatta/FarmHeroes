using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimney : MonoBehaviour
{
    int currentLevel = 0;
    public SpriteRenderer renderer;
    public Sprite[] sprites;

    void Start()
    {
        
    }

    public void Upgrade()
    {
        currentLevel++;
        renderer.sprite = sprites[currentLevel];
    }

    public void SetSprite(int index)
    {
        renderer.sprite = sprites[index];
    }
}
