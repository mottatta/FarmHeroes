using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButton : MonoBehaviour
{
    public EndOfMonthMenu menu;
    private void OnMouseDown()
    {
        menu.OnOkButtonClicked();
    }
}
