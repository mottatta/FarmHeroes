using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    bool gotoNextScene;
    public Animator animator;
    float nextSceneDelay = 0.6f;
    string anime;

    void Awake()
    {
        gameObject.SetActive(true);
        anime = "out";
        animator.Play("TransitionOut");
    }

    public void OnTransitionOutEnd()
    {
        if(anime == "out") gameObject.SetActive(false);
    }

    public void DisplayTransitionAndGotoNextScene()
    {
        anime = "in";
        gameObject.SetActive(true);
        gotoNextScene = true;
        animator.Play("TransitionIn");
    }

    public void GotoNextSceneWithDelay()
    {
        StartCoroutine(DelayedNextScene());
    }

    private IEnumerator DelayedNextScene()
    {
        yield return new WaitForSeconds(nextSceneDelay);
        GotoNextScene();
    }

    public void GotoNextScene()
    {
        if(gotoNextScene == true)
        {
            if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            //if no next scenes go to mainMenu
            {
                //TODO move GameComplete() in LastScene Start()
                //SharedState.GameComplete();
                SceneManager.LoadScene(1);
            }
        }
    }
}
