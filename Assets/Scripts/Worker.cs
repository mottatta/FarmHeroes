using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public Animator animator;
    public GameObject selectSymbol;
    public AudioClip sfxCut;
    public AudioClip sfxMine;
    LineRenderer lineRenderer;
    bool isSelected = false;
    WorkableObject currentWorkableObject;
    LevelManager levelManager;
    public int salary = 10;
    float animationSpeedBeforePause;

    public string action = "";

    public void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Workers";

        Select(false);
        Act("stay");
        animator.speed = 1;
    }

    public void BackToWork()
    {
        if (currentWorkableObject)
        {
            if(currentWorkableObject.type == "wood")
            {
                Act("cut");
            }
            else if(currentWorkableObject.type == "iron")
            {
                Act("mine");
            }
        }
    }

    public void PauseWorker()
    {
        animationSpeedBeforePause = animator.speed;
        animator.speed = 0;
    }

    public void UnpauseWorker()
    {
        animator.speed = 1; //animationSpeedBeforePause;
       // Act(action);
    }

    public void Act(string type)
    {
        action = type;
        string animationType = "WorkerStay";//default animation is WorkerStay 
        switch (type)
        {
            case "stay":
                animationType = "WorkerStay";
                break;
            case "cut":
                animationType = "WorkerCut";
                break;
            case "mine":
                animationType = "WorkerMine";
                break;
        }
        animator.Play(animationType);
    }

    public void OnMouseDown()
    {
        isSelected = !isSelected;
        Select(isSelected);
    }

    void Select(bool doSelect)
    {
        isSelected = doSelect;
        selectSymbol.SetActive(isSelected);
        if (isSelected) levelManager.OnWorkerSelected(this);
        else levelManager.OnWorkerUnselected();
    }

    public void MoveToMouse()
    {
        Select(false);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = transform.position.z;
        transform.position = newPos;
        LeaveWorkableObject();
    }

    public void GotoAndWork(WorkableObject wo)
    {
        if (currentWorkableObject != null) currentWorkableObject.currentWorker = null;
        Select(false);
        transform.position = new Vector3(wo.workerSpot.transform.position.x, wo.workerSpot.transform.position.y, transform.position.z);
        currentWorkableObject = wo;
        switch (wo.type)
        {
            case "wood":
                Act("cut");
                break;
            case "iron":
                Act("mine");
                break;
        }
    }

    private void Update()
    {
        if (isSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mousePos);
        }
        else lineRenderer.positionCount = 0;
    }

    public void LeaveWorkableObject()
    {
        if(currentWorkableObject != null) currentWorkableObject.currentWorker = null;
        currentWorkableObject = null;
        Act("stay");
    }

    public void OnHit()
    {
        if (SoundManager.GetInstance())
        {
            if (action == "cut") SoundManager.GetInstance().PlaySFX(sfxCut);
            else SoundManager.GetInstance().PlaySFX(sfxMine);
        }
        currentWorkableObject.OnHit();
    }
}
