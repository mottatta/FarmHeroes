using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public float secondsToProduction;
    public float secondsToReduceOnUpgrade;
    public int moneyPerProduction;
    public int moneyToIncreaseOnUpgrade;
    public Chimney[] chimneys;
    public Animator[] smokes;
    int slotsCount;
    public int ironPerProduction;
    public int woodPerProduction;
    public int woodPerProductionIncreased;
    public int woodToReduceOnUpgrade;
    bool isInProduction;
    public Transform[] moneySpots;
    public GameObject moneyPrefab;
    public LevelManager levelManager;
    public GameObject smoke;
    public UpgradeMenu upgradeMenu;
    public GameObject progressBarPrefab;
    public Transform consumeWoodPosition;
    public Transform consumeCoalPosition;
    public GameObject pointer;
    public Text lackOrResourceTxt;
    public float secondsToHideLackOfResourceTxt;
    bool factoryExplainerShown = false;

    void Start()
    {
        SetSlotsCount(1);
        DisablePointer();
        upgradeMenu.HideMenu();
    }

    public void OnCoalCollected()
    {
        if (!isInProduction)
        {
            if (ProductionAvailable()) StartProduction();
            else HandleLackOfResourcesReminder();
        }
    }

    public void OnWoodCollected()
    {
        if (!isInProduction)
        {
            if (ProductionAvailable()) StartProduction();
            else HandleLackOfResourcesReminder();
        }
    }

    bool ProductionAvailable()
    {
        return HasEnoughEnoughResources() && !AreThereNotCollectedMoney();
    }

    public void StartProduction()
    {
        if (!factoryExplainerShown)
        {
            factoryExplainerShown = true;
            levelManager.ShowExplainMenu(2);
            return;
        }
        levelManager.ConsumeCoal(ironPerProduction);
        levelManager.ConsumeWood(woodPerProduction);
        Invoke("Production", secondsToProduction);
        AddProgressBar();
        isInProduction = true;
        ActivateSmokes();
    }

    void StopProduction()
    {
        CancelInvoke("Production");
        isInProduction = false;
        DeactivateSmokes();
    }

    bool AreThereNotCollectedMoney()
    {
        Goods[] goods = GameObject.FindObjectsOfType<Goods>();
        foreach(Goods good in goods)
        {
            if (good.type == "money" && !good.isClicked) return true;
        }
        return false;
    }

    void ActivateSmokes()
    {
        for(int i = 0; i < smokes.Length;i++)
        {
            if (i < slotsCount)
            {
                smokes[i].Play("SmokeActive");
                smokes[i].speed = Random.Range(0.5f, 1f);
            }
        }
    }

    void DeactivateSmokes()
    {
        for (int i = 0; i < smokes.Length;i++)
        {
            smokes[i].Play("SmokeInvisible");
        }
    }

    void SetSlotsCount(int amount)
    {
        slotsCount = amount;
        for(int i = 0; i < chimneys.Length;i++)
        {
            if (i < slotsCount) chimneys[i].gameObject.SetActive(true);
            else chimneys[i].gameObject.SetActive(false);
        }
    }

    public void IncreaseSlotsCount()
    {
        slotsCount++;
        SetSlotsCount(slotsCount);
    }

    public void ReduceProductionTime()
    {
        secondsToProduction -= secondsToReduceOnUpgrade;
    }

    public void IncreaseProductionMoney()
    {
        moneyPerProduction += moneyToIncreaseOnUpgrade;
        for (int i = 0; i < chimneys.Length; i++) chimneys[i].Upgrade();
    }

    public void SetChimneysSprite(int index)
    {
        for (int i = 0; i < chimneys.Length; i++) chimneys[i].SetSprite(index);
    }

    public void ReduceProductionWood()
    {
        woodPerProduction -= woodToReduceOnUpgrade;
    }

    void AddProgressBar()
    {
        GameObject progressBar = Instantiate(progressBarPrefab);
        progressBar.GetComponent<ProgressBar>().SetSeconds(secondsToProduction);
        progressBar.transform.position = transform.position;
    }

    void Update()
    {
        
    }

    bool HasEnoughEnoughResources()
    {
        return levelManager.coal >= ironPerProduction && levelManager.wood >= woodPerProduction;
    }

    void Production()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            CreateGoods(i);
        }
        if (ProductionAvailable()) StartProduction();
        else
        {
            StopProduction();
            HandleLackOfResourcesReminder();
        }
    }    

    public void HandleLackOfResourcesReminder()
    {
        if (!factoryExplainerShown) return;
        if (levelManager.wood < woodPerProduction)
        {
            OnLackOfResource("wood");
            Invoke("HideLackOfResourceTxt", secondsToHideLackOfResourceTxt);
        }
        else if (levelManager.coal < ironPerProduction)
        {
            OnLackOfResource("coal");
            Invoke("HideLackOfResourceTxt", secondsToHideLackOfResourceTxt);
        }
    }

    void HideLackOfResourceTxt()
    {
        lackOrResourceTxt.text = "";
    }

    public void OnMoneyCollected()
    {
        if (!isInProduction && ProductionAvailable()) StartProduction();
    }

    void CreateGoods(int slotIndex)
    {
        CancelInvoke("CreateGoods");
        GameObject goods = Instantiate(moneyPrefab);
        Transform moneySpot = moneySpots[slotIndex];
        goods.transform.position = moneySpot.transform.position;
        Vector3 targetPos = GameObject.FindGameObjectWithTag("resourcesMenu").GetComponent<ResourcesMenu>().moneySpot.transform.position;
        goods.GetComponent<Goods>().SetValueAndTargetPos(moneyPerProduction, targetPos);
    }

    void OnLackOfResource(string type)
    {
        switch (type)
        {
            case "wood":
                lackOrResourceTxt.text = SharedState.GetJsonText("lack_of_wood", true);
                break;
            case "coal":
                lackOrResourceTxt.text = SharedState.GetJsonText("lack_of_coal", true);
                break;
        }
    }

    private void OnMouseDown()
    {
        DisablePointer();
        if (upgradeMenu.transform.position.x > 500f)
        {
            upgradeMenu.ShowMenu();
        }
        else
        {
            upgradeMenu.HideMenu();
        }
    }

    public void EnablePointer()
    {
        pointer.gameObject.SetActive(true);
    }

    void DisablePointer()
    {
        pointer.gameObject.SetActive(false);
    }
}
