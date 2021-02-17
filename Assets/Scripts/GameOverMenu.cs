using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text txt;
    void Start()
    {
        if (SharedState.LanguageDefs != null)
        {
            txt.text = SharedState.LanguageDefs["game_over_text"];
        }
        else txt.text = "Lorem epsum...";
        txt.text += " \n" + "$" + SharedState.Money;
        SharedState.GameComplete();
    }
}
