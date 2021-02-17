using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator.Play("FlameBurn", -1, Random.Range(0.0f, 1f));
    }

    void Update()
    {
        
    }
}
