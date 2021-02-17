using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkableObject : MonoBehaviour
{
    public int totalHits = 50;
    public int hitsToGoods = 5;
    public int currentHits = 0;
    public int value = 10;
    public string type = "wood";
    public Worker currentWorker;
    public Transform workerSpot;
    public Animator animator;
    LevelManager levelManager;
    public Transform goodsSpot;
    public GameObject goodsPrefab;
    public GameObject selector;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
        selector.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (currentWorker == null)
        {
            selector.SetActive(false);
            levelManager.OnWorkableObjectSelected(this);
        }
    }

    private void OnMouseEnter()
    {
        if(levelManager.currentWorker != null) selector.SetActive(true);
    }

    private void OnMouseExit()
    {
        selector.SetActive(false);
    }

    void CreateGoods()
    {
        GameObject goods = Instantiate(goodsPrefab);
        goods.transform.position = goodsSpot.transform.position;
        Vector3 targetPos = GameObject.FindGameObjectWithTag("resourcesMenu").GetComponent<ResourcesMenu>().woodSpot.transform.position;
        goods.GetComponent<Goods>().SetValueAndTargetPos(value, targetPos);
        if (currentWorker)
        {
            currentWorker.Act("stay");
            goods.GetComponent<Goods>().currentWorker = currentWorker;
        }
    }

    public void OnHit()
    {
        totalHits--;
        currentHits++;
        if(currentHits == hitsToGoods)
        {
            currentHits = 0;
            CreateGoods();
        }
        if(totalHits > 0)
        {
            animator.Play("TreeOnHit");
        }
        else
        {
            WorkerLeave();
            PlayOverAnimation();
            if(type == "wood")
            {
                if(GameObject.FindGameObjectsWithTag("tree").Length <= 1)
                {
                    GameObject.FindGameObjectWithTag("resourcesMenu").GetComponent<ResourcesMenu>().OnLackOfTrees();
                }
            }
            else if(type == "iron")
            {
                levelManager.UnlockIncreaseMoney();
            }
        }
    }

    public void WorkerLeave()
    {
        currentWorker.LeaveWorkableObject();
        currentWorker = null;
    }

    public void PlayOverAnimation()
    {
        animator.Play("TreeFadeOut");
    }
    

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
