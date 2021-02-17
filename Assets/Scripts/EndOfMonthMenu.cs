using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfMonthMenu : MonoBehaviour
{
    public Text endOfMonthTxt;
    public Text salary;
    public Text loan;
    public LevelManager levelManager;
    int totalExpense;

    private void Start()
    {
        EnableMenu();
    }


    public void EnableMenu()
    {
        gameObject.SetActive(true);
        if (SharedState.LanguageDefs != null)
        {
            endOfMonthTxt.text = SharedState.LanguageDefs["end_of_month"];
            salary.text = SharedState.LanguageDefs["salary"] + ": ";
            loan.text = SharedState.LanguageDefs["loan"] + ": ";
        }
        else
        {
            endOfMonthTxt.text = "End";
            salary.text = "sal:";
            loan.text = "lo:";
        }

        GameObject[] workers = GameObject.FindGameObjectsWithTag("worker");
        salary.text += workers.Length.ToString() + " x " + workers[0].GetComponent<Worker>().salary.ToString();
        loan.text += levelManager.loanPerMonth.ToString();
        totalExpense = workers.Length * workers[0].GetComponent<Worker>().salary;
        totalExpense += (int)levelManager.loanPerMonth;
    }

    public void OnOkButtonClicked()
    {
        levelManager.EndMonth(totalExpense);
        levelManager.HideEndOfMonthMenu();
    }

    public void DisableMenu()
    {
        gameObject.SetActive(false);
    }
}
