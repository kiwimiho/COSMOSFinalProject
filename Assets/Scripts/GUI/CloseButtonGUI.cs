using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonGUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseButton()
    {
        dialoguePanel.SetActive(false);
        audioSource.PlayOneShot(audioClip, 1.0f);
    }
}
