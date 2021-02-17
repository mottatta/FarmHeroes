using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeachButtonScript : MonoBehaviour
{
    
    [SerializeField] GameObject pointer;
    public AudioClip clickClip;
    float pointerDelay = 3f;
    public DescriptionText descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void ShowPointer()
    {
        pointer.SetActive(true);
    }

    void OnEnable()
    {
        pointer.SetActive(false);
        Invoke("ShowPointer", pointerDelay);
    }
    

    void OnMouseDown()
    {
        gameObject.SetActive(false);
        if(SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(clickClip);
        if (descriptionText) descriptionText.OnSpeachButtonPressed();
    }
}
