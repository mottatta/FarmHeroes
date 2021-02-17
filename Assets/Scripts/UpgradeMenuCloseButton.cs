using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuCloseButton : MonoBehaviour
{
    public UpgradeMenu upgradeMenu;
    public AudioClip sfxClicked;

    void OnMouseDown()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxClicked);
        upgradeMenu.HideMenu();
    }
}
