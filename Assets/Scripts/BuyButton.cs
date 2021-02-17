using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public string type;
    public int[] workersPrices;
    public int priceMoney;
    public int priceWood;
    public int priceFood;
    bool isAvailable;
    float unavailableAlpha = 0.20f;
    Animator animator;
    public ResourcesMenu resourceMenu;
    LevelManager levelManager;
    public AudioClip sfxMouseOver;
    public AudioClip sfxMouseDown;
    string desc;
    public GameObject pointer;

    public SpriteRenderer[] renderers;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        MakeUnavailable();
        SetDesc();
        pointer.SetActive(false);
    }

    void SetDesc()
    {
        desc = "";
        desc += GetJsonText("cost") + " \n";
        if(type == "worker")
        {
            priceMoney = workersPrices[GameObject.FindGameObjectsWithTag("worker").Length];
        }
        desc += priceMoney.ToString() + " " + GetJsonText("money") + " \n";
        desc += GetJsonText("description_" + type);
        if (type == "credit") desc = GetJsonText("description_credit");
    }

    void OnMouseEnter()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxMouseOver);
        SetDesc();
        resourceMenu.ShowDescription(desc, transform.position.y);
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

    void OnMouseExit()
    {
        resourceMenu.HideDescription();
    }

    private void OnMouseDown()
    {
        if (isAvailable)
        {
            pointer.SetActive(false);
            animator.Play("BuyButtonClick");
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxMouseDown);
            switch (type)
            {
                case "worker":
                    levelManager.OnWorkerBuyButtonClicked(priceMoney, priceWood, priceFood);
                    break;
                case "tree":
                    levelManager.OnWoodBuyButtonClicked(priceMoney, priceWood, priceFood);
                    break;
                case "credit":
                    if (!levelManager.isCreditExplainShown) levelManager.ShowCreditExplain();
                    levelManager.OnCreditClicked();
                    break;
            }
        }
    }

    void MakeAvailable()
    {
        isAvailable = true;
        foreach(SpriteRenderer renderer in renderers)
        {
            var tempColor = renderer.color;
            tempColor.a = 1;
            renderer.color = tempColor;
        }
    }

    void MakeUnavailable()
    {
        isAvailable = false;
        foreach (SpriteRenderer renderer in renderers)
        {
            var tempColor = renderer.color;
            tempColor.a = unavailableAlpha;
            renderer.color = tempColor;
        }
    }

    void Update()
    {
        if(type == "credit")
        {
            if (levelManager.loan <= 0 && levelManager.GetMonthsLeft() <= levelManager.totalMonths -1)
            {
                if (!isAvailable)
                {
                    MakeAvailable();
                }
            }
            else if (isAvailable) MakeUnavailable();
        }
        else
        if (levelManager.money >= priceMoney)
        {
            if (!isAvailable) MakeAvailable();
        }
        else
        {
            if (isAvailable) MakeUnavailable();
        }
    }
}
