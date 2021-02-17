using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingLabel : MonoBehaviour
{
    public Color colorAdd;
    public Color colorSub;
    public Sprite imageMoney;
    public Sprite imageWood;
    public Sprite imageCoal;
    public SpriteRenderer bodyRenderer;
    public Text txt;

    public void SetVal(int val, string type)
    {
        if(val > 0)
        {
            txt.color = colorAdd;
            txt.text = "+";
        }
        else
        {
            txt.color = colorSub;
        }
        txt.text += val.ToString();
        switch (type)
        {
            case "money":
                bodyRenderer.sprite = imageMoney;
                break;
            case "wood":
                bodyRenderer.sprite = imageWood;
                break;
            case "coal":
                bodyRenderer.sprite = imageCoal;
                break;
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
