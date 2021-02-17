using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public Description description;
    public AudioClip sfxShow;
    public UpgradeButton[] buttons;
    public Factory factory;
    public LevelManager levelManager;
    Vector3 startPosition;
    void Awake()
    {
        startPosition = transform.position;
    }

    public void DisplayDescription(string str, float xPos)
    {
        //description.transform.position = new Vector2(xPos, description.transform.position.y);
        description.SetTextByStringAndShow(str, xPos);
    }

    void SetXPos(GameObject obj, float xPos)
    {
        obj.transform.position = new Vector3(xPos, obj.transform.position.y, obj.transform.position.z);
    }

    public void HideDescription()
    {
        description.Hide();
    }

    public void ShowMenu()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxShow);
        //gameObject.SetActive(true);
        transform.position = startPosition;
        DisableAllButtonPointers();
        EnableAvailableButtonPointer();
    }

    public void EnableAvailableButtonPointer()
    {
        if (buttons[6].IsAvailable()) {
            buttons[6].EnablePointer();
            return;
        }
        else if (buttons[7].IsAvailable()) {
            buttons[7].EnablePointer();
            return;
        }
        else
        for (int i = 0; i < buttons.Length - 2; i++)
        {
            if (buttons[i].IsAvailable())
            {
                buttons[i].EnablePointer();
                return;
            }
        }
    }

    public void HideMenu()
    {
        description.Hide();
        //gameObject.SetActive(false);
        transform.position = new Vector3(1000, 1000, 0);
    }

    void DisableAllButtonPointers()
    {
        foreach(UpgradeButton button in buttons)
        {
            button.DisablePointer();
        }
    }

    void Update()
    {
        if (HasAvailableUpdate() && !levelManager.isSavingExplainShown) levelManager.ShowSavingsExplain();
        if(transform.position.x > 500 && HasAvailableUpdate())
        {
            if (!factory.pointer.activeInHierarchy) factory.EnablePointer();
        }
    }

    bool HasAvailableUpdate()
    {
        for (int i = 0; i < buttons.Length; i++) if (buttons[i].IsAvailable()) return true;
        return false;
    }
}
