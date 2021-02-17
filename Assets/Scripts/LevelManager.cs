using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Worker currentWorker;

    public int wood;
    public int money;
    public int food;
    public int coal;

    public GameObject workerPrefab;
    public GameObject preTreePrefab;
    public GameObject preFarmPrefab;
    public GameObject preWorkerPrefab;
    public GameObject fadingLabelPrefab;
    public Transform workerSpot;
    public Factory factory;
    public ExplainMenu explainMenu;
    public TransitionScript transition;
    public Instructions instructions;
    public EndOfMonthMenu endOfMonthMenu;
    public CreditMenu creditMenu;
    public Text daysDisplay;
    public Disabler disabler;
    Vector2 fadingLabelPosition;

    public bool collectInstructionsShown = false;

    public int totalMonths;
    int currentMonth = 0;
    public int durationToProgress = 30;
    public int countSeconds = 0;

    public int loan = 0;
    public int loanPerMonth = 0;

    float timer = 0f;
    public int secondsPerMonth;
    public int secondsPerDay = 4;

    bool isPaused = false;

    int tutorial_CollectWoodIndex = 2;
    int moneyUpgrade = 0;

    public bool isDoubleMoneyUpgradeLocked = true;
    public bool isTripleMoneyUpgradeLocked = true;
    public bool isSavingExplainShown = false;
    public bool isCreditExplainShown = false;
    public AudioClip sfxCredit;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnEverySecond", 1f);
        //instructions.ShowAndDisplayText("instructions_mark");
        //if(disabler.gameObject.activeInHierarchy) disabler.CreateNextTutorial();
        ShowExplainMenu(1);
    }

    public void CreateDisablareNextTutorial()
    {
        disabler.CreateNextTutorial();
    }

    public void UnlockIncreaseMoney()
    {
        if (isDoubleMoneyUpgradeLocked)
        {
            isDoubleMoneyUpgradeLocked = false;
            return;
        }
        else if (isTripleMoneyUpgradeLocked)
        {
            isTripleMoneyUpgradeLocked = false;
            factory.ironPerProduction = 0;
            factory.woodPerProduction = factory.woodPerProductionIncreased;
            return;
        }
    }

    void OnEverySecond()
    {
        countSeconds++;
        if(countSeconds >= durationToProgress)
        {
            countSeconds = 0;
            SharedState.SubmitProgress();
        }
        Invoke("OnEverySecond", 1f);
    }

    public void OnGoodsCreated(Goods obj)
    {
        if (!collectInstructionsShown)
        {
            collectInstructionsShown = true;
            disabler.OnResourceCreated(obj.gameObject);
        }
    }

    public void OnGoodsCollected(Vector2 pos)
    {
        fadingLabelPosition = pos;
        instructions.Hide();
    }

    public void ShowExplainMenu(int index)
    {
        explainMenu.ShowMenu(index);
    }

    public void ShowSavingsExplain()
    {
        isSavingExplainShown = true;
        explainMenu.ShowMenu(7);
    }

    public void ShowCreditExplain()
    {
        isCreditExplainShown = true;
        explainMenu.ShowMenu(8);
    }

    public void HideExplainMenu()
    {
        explainMenu.HideMenu();
    }

    public void Upgrade(int type)
    {
        switch (type)
        {
            case 0:
                factory.IncreaseSlotsCount();
                factory.SetChimneysSprite(moneyUpgrade);
                break;
            case 1:
                factory.ReduceProductionTime();
                break;
            case 2:
                factory.IncreaseSlotsCount();
                factory.SetChimneysSprite(moneyUpgrade);
                break;
            case 3:
                factory.ReduceProductionTime();
                break;
            case 4:
                factory.ReduceProductionWood();
                break;
            case 5:
                factory.ReduceProductionWood();
                break;
            case 6:
                moneyUpgrade++;
                factory.IncreaseProductionMoney();
                break;
            case 7:
                moneyUpgrade++;
                factory.IncreaseProductionMoney();
                break;
        }
    }

    void GameOver()
    {
        SharedState.Money = money;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnGamePlayAreaClicked()
    {
        if (currentWorker != null) currentWorker.MoveToMouse();
    }

    public void OnWorkerBuyButtonClicked(int priceMoney, int priceWood, int priceFood)
    {
        GameObject worker = Instantiate(preWorkerPrefab);
        preWorkerPrefab.GetComponent<PreTree>().SetPrices(priceMoney, priceWood, priceFood);
    }

    public void OnWoodBuyButtonClicked(int priceMoney, int priceWood, int priceFood)
    {
        GameObject preTree = Instantiate(preTreePrefab);
        preTree.GetComponent<PreTree>().SetPrices(priceMoney, priceWood, priceFood);
    }

    public void PurchaseComplete(int priceMoney, int priceWood, int priceFood)
    {
        money -= priceMoney;
        wood -= priceWood;
        food -= priceFood;
    }

    public void OnCreditClicked()
    {
        creditMenu.ShowMenu();
    }

    public void OnFarmClicked(int priceMoney, int priceWood, int priceFood)
    {
        GameObject preFarm = Instantiate(preFarmPrefab);
        preFarm.GetComponent<PreTree>().SetPrices(priceMoney, priceWood, priceFood);
    }

    public void CollectCoal(int amount)
    {
        GenerateFadingLabel(amount, "coal", fadingLabelPosition);
        fadingLabelPosition = Vector2.zero;
        coal += amount;
        factory.OnCoalCollected();
    }

    public void ConsumeMoney(int amount)
    {
        money -= amount;
        GenerateFadingLabel(amount * -1, "money", factory.consumeCoalPosition.position);
    }

    public void ConsumeCoal(int amount)
    {
        coal -= amount;
        GenerateFadingLabel(amount * -1, "coal", factory.consumeCoalPosition.position);
    }

    public void ConsumeWood(int amount)
    {
        wood -= amount;
        GenerateFadingLabel(amount * -1, "wood", factory.consumeWoodPosition.position);
        fadingLabelPosition = Vector2.zero;
    }

    public void CollectWood(int amount)
    {
        GenerateFadingLabel(amount, "wood", fadingLabelPosition);
        fadingLabelPosition = Vector2.zero;
        wood += amount;
        factory.OnWoodCollected();

        Debug.Log(disabler.currentTutorialIndex);
        if (disabler.currentTutorialIndex == tutorial_CollectWoodIndex)
        {
            collectInstructionsShown = false;
            disabler.CreateNextTutorial();
        }
    }

    public void CollectMoney(int amount)
    {
        GenerateFadingLabel(amount, "money", fadingLabelPosition);
        fadingLabelPosition = Vector2.zero;
        money += amount;
        factory.OnMoneyCollected();
    }

    public void CollectFood(int amount)
    {
        food += amount;
    }

    public void OnWorkerSelected(Worker worker)
    {
        currentWorker = worker;
    }

    public void OnWorkerUnselected()
    {
        currentWorker = null;
    }

    public void OnWorkableObjectSelected(WorkableObject wo)
    {
        if(currentWorker != null)
        {
            instructions.Hide();
            wo.currentWorker = currentWorker;
            currentWorker.GotoAndWork(wo);
        }
    }

    public void EndMonth(int expenses)
    {
        if(GetMonthsLeft() == totalMonths - 1) ShowExplainMenu(9);//when 1 month is passed credit button will be unlocked, so show tutorial for that
        money -= expenses;
        loan -= (int)loanPerMonth;
        GenerateFadingLabel(expenses * -1, "money", Vector2.zero);
        if(currentMonth >= totalMonths)
        {
            GameOver();
        }
    }

    void GenerateFadingLabel(int val, string type, Vector2 position)
    {
        GameObject fadingLabel = Instantiate(fadingLabelPrefab);
        if (position != Vector2.zero) fadingLabel.transform.position = position;
        fadingLabel.GetComponent<FadingLabel>().SetVal(val, type);
    }

    void ShowEndOfMonthMenu()
    {
        PauseGame();
        endOfMonthMenu.EnableMenu();
    }

    public void HideEndOfMonthMenu()
    {
        UnpauseGame();
        endOfMonthMenu.DisableMenu();
    }

    public void PauseGame(bool pauseWorkers = false)
    {
        isPaused = true;
        if(pauseWorkers) PauseWorkers();
    }

    public void UnpauseGame()
    {
        isPaused = false;
        UnpauseWorkers();
    }

    void PauseWorkers()
    {
        GameObject[] workers = GameObject.FindGameObjectsWithTag("worker");
        foreach(GameObject worker in workers)
        {
            worker.GetComponent<Worker>().PauseWorker();
        }
    }

    void UnpauseWorkers()
    {
        GameObject[] workers = GameObject.FindGameObjectsWithTag("worker");
        foreach (GameObject worker in workers)
        {
            worker.GetComponent<Worker>().UnpauseWorker();
        }
    }

    public void TakeCredit(int amount, int amountToReturn)
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxCredit);
        loan += amount;
        money += amount;
        
        loanPerMonth = (int)(Mathf.Ceil((float)amountToReturn / GetMonthsLeft()));
        Debug.Log("loan per month " + loanPerMonth.ToString());
        GenerateFadingLabel(amount, "money", new Vector2(-2, 0));
    }

    public float GetMonthsLeft()
    {
        return totalMonths - currentMonth;
    }

    void Update()
    {
        if (!isPaused)
        {
            timer += Time.deltaTime;
            DisplayDay();
            if(timer >= secondsPerMonth)
            {
                currentMonth++;
                ShowEndOfMonthMenu();
                timer = 0;
            }
        }
    }

    void DisplayDay()
    {
        int day = (int)(timer / secondsPerDay);
        day = Mathf.Max(day, 1);
        daysDisplay.text = "Day: " + day.ToString();
    }
}
