using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButtonCreditMenu : MonoBehaviour
{
    public string type;
    public CreditMenu menu;
    private void OnMouseDown()
    {
        switch (type)
        {
            case "ok":
                menu.OnOkButtonPressed();
                break;
            case "cancel":
                menu.OnCancelButtonPressed();
                break;
        }
    }
}
