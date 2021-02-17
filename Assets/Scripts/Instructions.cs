using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public Text txt;
    void Start()
    {
        
    }

    public void ShowAndDisplayText(string key)
    {
        Show();
        if (SharedState.LanguageDefs != null)
        {
            txt.text = SharedState.LanguageDefs[key];
        }
        else txt.text = "Lorem epsum";
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
