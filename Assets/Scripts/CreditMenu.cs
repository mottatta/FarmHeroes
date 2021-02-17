using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMenu : MonoBehaviour
{
    public Text instructionsTxt;
    public Text amountTxt;
    public Text amountValTxt;
    public Text amountToReturnTxt;
    public Text amountToReturnValTxt;
    public Text periodTxt;
    public Text periodVal;
    public ArrowButton upButton;
    public ArrowButton downButton;
    public LevelManager levelManager;

    public int amount;
    public int amountToReturn;
    public int increasePerStep;
    public int minAmount;
    public int maxAmount;
    public int creditPercent;

    private void Start()
    {
        SetTxtValues();
    }

    public void OnUpButtonPressed()
    {
        if (amount + increasePerStep <= maxAmount) amount += increasePerStep;
        SetTxtValues();
    }

    public void OnDownButtonPressed()
    {
        if (amount - increasePerStep >= minAmount) amount -= increasePerStep;
        SetTxtValues();
    }

    void SetTxtValues()
    {
        instructionsTxt.text = GetJsonText("credit_instructions") + " \n";
        amountTxt.text = GetJsonText("amount") + ": ";
        amountValTxt.text = amount.ToString();
        amountToReturnTxt.text = GetJsonText("amountToReturn") + ": ";
        amountToReturn = amount + ((amount / 100) * creditPercent);
        amountToReturnValTxt.text = amountToReturn.ToString();
        periodTxt.text = GetJsonText("credit_period") + ": ";
        periodVal.text = levelManager.GetMonthsLeft().ToString() + GetJsonText("months");
    }

    private void Update()
    {
        if (amount + increasePerStep > maxAmount) upButton.DisableButton();
        else upButton.EnableButton();
        if (amount - increasePerStep < minAmount) downButton.DisableButton();
        else downButton.EnableButton();
    }

    public void ShowMenu()
    {
        levelManager.PauseGame(true);
        amount = minAmount;
        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        levelManager.UnpauseGame();
    }

    public void OnOkButtonPressed()
    {
        HideMenu();
        levelManager.TakeCredit(amount, amountToReturn);
    }

    public void OnCancelButtonPressed()
    {
        HideMenu();
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
}
