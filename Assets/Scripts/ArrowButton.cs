using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public string type;
    public CreditMenu menu;
    public SpriteRenderer renderer;
    public Animator animator;
    public Color enabledColor;
    public Color disabledColor;
    public bool isEnabled;
    public AudioClip sfxClick;

    private void OnMouseDown()
    {
        if (isEnabled)
        {
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxClick);
            animator.StopPlayback();
            animator.Play("UpgradeButtonClick");
            switch (type)
            {
                case "up":
                    menu.OnUpButtonPressed();
                    break;
                case "down":
                    menu.OnDownButtonPressed();
                    break;
            }
        }
    }

    public void EnableButton()
    {
        isEnabled = true;
        renderer.color = enabledColor;
    }

    public void DisableButton()
    {
        isEnabled = false;
        renderer.color = disabledColor;
    }
}
