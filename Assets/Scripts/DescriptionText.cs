using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class DescriptionText : MonoBehaviour
{
    public Text txt;
    public float delayBetweenLetters = 0.03f;
    public GameObject speachButton;
    public Coroutine c;
    public ExplainMenu explainMenu;

    public virtual void DisplayText(string key, bool showSpeachButtonOnComplete = false)
    {
        if (!gameObject.activeInHierarchy) return;
        if (speachButton) speachButton.SetActive(false);
        txt.text = "";
        if (c != null) StopCoroutine(c);
        c = null;
        string textKey = key;
        if(SharedState.LanguageDefs != null)
        {
            if (SharedState.LanguageDefs[textKey] != null)
            {
                LOLSDK.Instance.SpeakText(textKey);
                c = StartCoroutine(AnimateText(SharedState.LanguageDefs[textKey], showSpeachButtonOnComplete));
            }
            else c = StartCoroutine(AnimateText("Lorem epsum...", showSpeachButtonOnComplete));
        }
        else c = StartCoroutine(AnimateText("Lorem epsum...", showSpeachButtonOnComplete));
    }

    public virtual IEnumerator AnimateText(string inputText, bool showSpeachButtonOnComplete)
    {
        int i = 0;
        while (i < inputText.Length)
        {
            txt.text += inputText[i];
            i++;
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        if(showSpeachButtonOnComplete) speachButton.SetActive(true);
    }

    public void SetText(string t)
    {
        txt.text = t;
    }

    public virtual void OnSpeachButtonPressed()
    {
        if (explainMenu) explainMenu.OnSpeachButtonPressed();
    }
}
