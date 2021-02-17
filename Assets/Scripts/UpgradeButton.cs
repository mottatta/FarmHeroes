using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public SpriteRenderer topSymbol;
    public Sprite lockedSprite;
    public Sprite purchasedSprite;
    public Sprite availableSprite;
    public AudioClip sfxSelect;
    public AudioClip sfxPurchase;

    LevelManager levelManager;
    public int type;
    public int prizeMoney;
    public int prizeWood;
    public GameObject pointer;
    public UpgradeMenu upgradeMenu;
    bool isPurchased = false;
    string desc = "";
    Animator animator;
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        SetDescriptionString();
    }

    void SetDescriptionString()
    {
        if (type != 6 && type != 7)
        {
            desc += GetJsonText("cost") + " \n";
            desc += GetJsonText("money") + ": " + prizeMoney.ToString() + " \n";
            desc += GetJsonText("wood") + ": " + prizeWood.ToString() + " \n \n";
        }
        else
        {
            if(type == 6)
            {
                if (levelManager.isDoubleMoneyUpgradeLocked) desc += GetJsonText("locked");
                else desc += GetJsonText("unlocked");
            }
            else if(type == 7)
            {
                if (levelManager.isTripleMoneyUpgradeLocked) desc += GetJsonText("locked");
                else desc += GetJsonText("unlocked");
            }
            desc += " \n";
        }
        desc += GetJsonText("upgrade_" + type.ToString());
    }

    string GetJsonText(string key)
    {
        if (SharedState.LanguageDefs != null)
        {
            if (SharedState.LanguageDefs[key] != null) return SharedState.LanguageDefs[key];
            Debug.LogError("No such jason text " + key);
            return "Lorem epsum";
        }
        else return "Lorem epsum";
    }

    public void ShowTopSprite()
    {
        if (isPurchased) topSymbol.sprite = purchasedSprite;
        else if (IsAvailable()) topSymbol.sprite = availableSprite;
        else topSymbol.sprite = lockedSprite;
    }

    private void OnMouseEnter()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxSelect);
        float xPos = transform.position.x;
        if (type == 7) xPos -= 1.15f;
        upgradeMenu.DisplayDescription(desc, xPos);
    }



    private void OnMouseExit()
    {
        //upgradeMenu.HideDescription();
    }

    private void OnMouseDown()
    {
        if(IsAvailable()) Purchase();
    }

    void Purchase()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxPurchase);
        isPurchased = true;
        if(prizeMoney > 0) levelManager.ConsumeMoney(prizeMoney);
        if(prizeWood > 0) levelManager.ConsumeWood(prizeWood);
        levelManager.Upgrade(type);
        animator.Play("UpgradeButtonClick");
        DisablePointer();
        upgradeMenu.EnableAvailableButtonPointer();
        switch (type)
        {
            case 0:
            case 2:
                levelManager.ShowExplainMenu(3);
                break;
            case 1:
            case 3:
                levelManager.ShowExplainMenu(4);
                break;
            case 6:
                levelManager.ShowExplainMenu(5);
                break;
            case 7:
                levelManager.ShowExplainMenu(6);
                break;
        }
    }

    public bool IsAvailable()
    {
        if (isPurchased) return false;
        if(type == 6)
        {
            if (levelManager.isDoubleMoneyUpgradeLocked) return false;
            return true;
        }
        if (type == 7)
        {
            if (levelManager.isTripleMoneyUpgradeLocked) return false;
            return true;
        }
        if (levelManager.money >= prizeMoney && levelManager.wood >= prizeWood) return true;
        return false;
    }

    void Update()
    {
        ShowTopSprite();
    }

    public void EnablePointer()
    {
        pointer.gameObject.SetActive(true);
    }

    public void DisablePointer()
    {
        pointer.gameObject.SetActive(false);
    }
}
