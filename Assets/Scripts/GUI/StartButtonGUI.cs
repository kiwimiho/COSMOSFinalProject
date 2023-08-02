using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonGUI : MonoBehaviour
{
    public GameObject startScreen;
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        startScreen.SetActive(false);
        audioSource.PlayOneShot(audioClip, 1.0f);
    }
}
