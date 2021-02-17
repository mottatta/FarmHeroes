using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public Text txt;
    Animator animator;
    string animation;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetTextAndShow(string key, float yPos)
   {
        
            Debug.Log("Show");
            if (SharedState.LanguageDefs != null)
            {
                txt.text = SharedState.LanguageDefs[key];
            }
            else txt.text = "Lorem epsum";
            animator.StopPlayback();
            animator.Play("DescriptionShow1");
            animation = "show";
            this.transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    public void SetTextByStringAndShow(string str, float xPos, bool pos = true, bool setYPos = false)
    {
        txt.text = str;
        animator.StopPlayback();
        animator.Play("DescriptionShow1");
        animation = "show";
        if(pos) this.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        else if (setYPos) this.transform.position = new Vector3(transform.position.x, xPos, transform.position.z);
    }


    public void Hide()
    {
        Debug.Log("Hide");
        animator.Play("DescriptionHide");
        animation = "hide";
    }
}
