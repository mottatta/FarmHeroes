using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disabler : MonoBehaviour
{
    public DescriptionText txt;
    public GameObject[] tutorialObjects;
    public GameObject currentFocusedObject;
    public GameObject mask;
    public float delayBetweenTutorials = 0.50f;
    public float maskSpeed = 20f;
    public int currentTutorialIndex = -1;
    Vector3 originalPos;
    Vector3 targetPos;
    bool isOneTimeTutorial = false;
    bool isMaskInPlace = false;
    bool showSpeachButtonOnDialogComplete = false;
    bool disableSpriteRenderer = false;
    float focusedObjectZPosition;
    public SpriteRenderer renderer;
    public LevelManager levelManager;


    void Start()
    {
        originalPos = transform.position;
    }

    public void OnTutorialTaskComplete()
    {
        if(currentFocusedObject != null) UnfocusObject(currentFocusedObject);
        currentFocusedObject = null;
        if (isOneTimeTutorial)
        {
            isOneTimeTutorial = false;
            HideDisabler();
        }
        else
        {
            switch (currentTutorialIndex)
            {
                case 1:
                case 4:
                    disableSpriteRenderer = true;
                    Invoke("CreateNextTutorial", delayBetweenTutorials);
                    break;
                default:
                    Invoke("CreateNextTutorial", delayBetweenTutorials);
                    break;
            }
        }
    }

    public void CreateNextTutorial()
    {
        gameObject.SetActive(true);
        ShowDisabler();
        renderer.enabled = !disableSpriteRenderer;
        isOneTimeTutorial = false;
        currentTutorialIndex++;
        if (tutorialObjects.Length - 1 >= currentTutorialIndex)
        {
            FocusOnObject(tutorialObjects[currentTutorialIndex]);
            string key = "tutorial_" + currentTutorialIndex.ToString();
            txt.DisplayText(key, showSpeachButtonOnDialogComplete);
        }
        else Debug.LogError("Tutorial object index out of bounds!");
    }

    public void FocusOnObject(GameObject o, string taskCompleteAction = "mouseClick")
    {
        if (currentFocusedObject != null) UnfocusObject(currentFocusedObject);
        if(o != null)
        {
            isMaskInPlace = false;
            currentFocusedObject = o;
            currentFocusedObject.GetComponent<TutorialObject>().disabler = this;
            focusedObjectZPosition = currentFocusedObject.transform.position.z;//remember the z position and restore when lose focus of this object
            GameObject centerSpot = currentFocusedObject.GetComponent<TutorialObject>().GetCenterPoint(taskCompleteAction);
            targetPos = centerSpot.transform.position;
            SetZPosition(currentFocusedObject, transform.position.z - 1);
        }
    }

    void UnfocusObject(GameObject o)
    {
        //return old z position of the previous focused object
        o.GetComponent<TutorialObject>().disabler = null;
        SetZPosition(o, focusedObjectZPosition);
    }

    void SetZPosition(GameObject o, float zPos)
    {
        o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y, zPos);
    }

    void MaskReachedTargetPos()
    {
        isMaskInPlace = true;
    }

    public void OnResourceCreated(GameObject resource)
    {
        disableSpriteRenderer = false;
        renderer.enabled = true;
        ShowDisabler(true);
        isOneTimeTutorial = true;
        FocusOnObject(resource, "mouseEnter");
        string key = "tutorial_collect_resource";
        txt.DisplayText(key, false);
    }

    void HideDisabler()
    {
        transform.position = new Vector3(-500, transform.position.y, transform.position.z);
        levelManager.UnpauseGame();
    }

    void ShowDisabler(bool pauseWorkers = false)
    {
        transform.position = originalPos;
        levelManager.PauseGame(pauseWorkers);
    }

    void Update()
    {
        if(!isMaskInPlace && targetPos != mask.transform.position)
        {
            //Move mask to targetPos
            mask.transform.position = Vector3.MoveTowards(mask.transform.position, targetPos, maskSpeed * Time.deltaTime);
            if(mask.transform.position == targetPos)
            {
                MaskReachedTargetPos();
            }
        }
    }
}
