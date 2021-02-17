using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainMenu : MonoBehaviour
{
    int explainIndex;
    int currentExplain;
    public DescriptionText descriptionText;
    public Animator animator;
    public LevelManager levelManager;
    public BoxCollider2D collider;
    public Factory factory;
    public void ShowMenu(int index)
    {
        animator.Play("ExplainerShow");
        gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
        collider.enabled = true;
        levelManager.PauseGame(true);
        explainIndex = index;
        currentExplain = 1;
        string key = "explain_" + explainIndex.ToString() + "_" + currentExplain.ToString();
        descriptionText.DisplayText(key, true);
    }

    public void HideMenu()
    {
        factory.HandleLackOfResourcesReminder();
        factory.upgradeMenu.HideMenu();
        if (explainIndex == 2) levelManager.factory.StartProduction();
        collider.enabled = false;
        levelManager.UnpauseGame();
        animator.Play("ExplainerHide");
    }

    public void OnSpeachButtonPressed()
    {
        currentExplain++;
        string key = "explain_" + explainIndex.ToString() + "_" + currentExplain.ToString();
        if (SharedState.LanguageDefs != null)
        {
            if (SharedState.LanguageDefs[key] != null)
            {
                descriptionText.DisplayText(key, true);
            }
            else SpeachDoesntExist();
        }
        else SpeachDoesntExist();
    }

    void SpeachDoesntExist()
    {
        if (explainIndex == 1) levelManager.CreateDisablareNextTutorial();
        HideMenu();
    }
}
