using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public float foodInterval = 3f;
    public int value = 1;
    public GameObject foodPrefab;
    public GameObject foodSpot;
    void Start()
    {
        Invoke("CreateGoods", foodInterval);
    }

    void Update()
    {
        
    }

    void CreateGoods()
    {
        CancelInvoke("CreateGoods");
        GameObject goods = Instantiate(foodPrefab);
        goods.transform.position = foodSpot.transform.position;
        Vector3 targetPos = GameObject.FindGameObjectWithTag("resourcesMenu").GetComponent<ResourcesMenu>().foodSpot.transform.position;
        goods.GetComponent<Goods>().SetValueAndTargetPos(value, targetPos);
        Invoke("CreateGoods", foodInterval);
    }

    void Sell()
    {
        CancelInvoke("CreateGoods");
    }
}
