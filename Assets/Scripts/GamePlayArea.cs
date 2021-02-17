using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayArea : MonoBehaviour
{
    LevelManager levelManager;
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
    }

    private void OnMouseDown()
    {
        levelManager.OnGamePlayAreaClicked();
    }
}
