using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] TransitionScript transitionScript;
    public AudioClip clickAudio;
    void OnMouseDown()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(clickAudio);
        transitionScript.DisplayTransitionAndGotoNextScene(); 
    }
}
