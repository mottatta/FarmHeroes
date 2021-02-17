using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesMenu : MonoBehaviour
{
    public Text woodTxt;
    public Text moneyTxt;
    public Text foodTxt;
    public Text ironTxt;
    public Text creditTxt;
    public Transform woodSpot;
    public Transform moneySpot;
    public Transform foodSpot;
    public Transform ironSpot;
    public Description description;

    public BuyButton treeButton;
    
    LevelManager levelManager;
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        woodTxt.text = levelManager.wood.ToString();
        moneyTxt.text = levelManager.money.ToString();
        ironTxt.text = levelManager.coal.ToString();
        creditTxt.text = SharedState.GetJsonText("credit") + ": \n" + levelManager.loan.ToString();
    }

    public void OnLackOfTrees()
    {
        treeButton.pointer.SetActive(true);
    }

    public void ShowDescription(string str, float yPos)
    {
        description.SetTextByStringAndShow(str, yPos, false, true);
    }

    public void HideDescription()
    {
        description.Hide();
    }
}
