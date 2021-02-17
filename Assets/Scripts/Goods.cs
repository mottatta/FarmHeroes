using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public int value;
    public string type;
    float moveSpeed = 40f;
    public bool isClicked = false;
    public float timeToSelfDestruct = 3f;
    Vector3 targetPos = Vector3.zero;
    LevelManager levelManager;
    public Worker currentWorker;

    public AudioClip sfxFall;
    public AudioClip sfxClicked;
    public AudioClip sfxMoney;
    public AudioClip sfxCollect;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        levelManager.OnGoodsCreated(this);
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxFall);
        //Invoke("DestroyObject", timeToSelfDestruct);
    }

    public void SetValueAndTargetPos(int v, Vector2 t)
    {
        value = v;
        targetPos = t;
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            Clicked();
        }
    }

    private void OnMouseEnter()
    {
        if (!isClicked)
        {
            Clicked();
            if (currentWorker != null) currentWorker.BackToWork();
        }
    }

    void Clicked()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxClicked);
        levelManager.OnGoodsCollected(transform.position);
        isClicked = true;
        CancelInvoke("DestroyObject");
    }

    void Update()
    {
        if (isClicked)
        {
            if (transform.position!= targetPos)
            {
                Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                transform.position = newPos;
            }
            else Collect();
        }
    }

    void Collect()
    {
        switch (type)
        {
            case "wood":
                if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxCollect);
                levelManager.CollectWood(value);
                break;
            case "money":
                if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxMoney);
                levelManager.CollectMoney(value);
                break;
            case "food":
                levelManager.CollectFood(value);
                break;
            case "iron":
                if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxCollect);
                levelManager.CollectCoal(value);
                break;
        }
        Destroy(gameObject);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
