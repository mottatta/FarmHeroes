using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public GameObject centerSpot;
    public Disabler disabler;
    bool isClicked = true;
    string taskCompleteAction;

    public GameObject GetCenterPoint(string _taskCompleteAction = "mouseClick")
    {
        taskCompleteAction = _taskCompleteAction;
        isClicked = false;
        return centerSpot;
    }

    private void OnMouseEnter()
    {
        if (!isClicked && taskCompleteAction == "mouseEnter")
        {
            isClicked = true;
            disabler.OnTutorialTaskComplete();
        }
    }

    private void OnMouseDown()
    {
        if (!isClicked && taskCompleteAction == "mouseClick")
        {
            isClicked = true;
            disabler.OnTutorialTaskComplete();
        }
    }
}
