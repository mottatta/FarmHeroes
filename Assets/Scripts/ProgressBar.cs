using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    float totalSeconds = 10;
    float currentSeconds = 0;
    public Image bar;

    public void SetSeconds(float seconds)
    {
        totalSeconds = seconds;
        currentSeconds = 0;
    }

    private void Update()
    {
        currentSeconds += Time.deltaTime;
        float percent = currentSeconds / totalSeconds;
        bar.fillAmount = percent;
        if (currentSeconds >= totalSeconds) Destroy(gameObject);
    }
}
