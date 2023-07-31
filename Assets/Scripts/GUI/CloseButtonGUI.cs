using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonGUI : MonoBehaviour
{
    public GameObject dialoguePanel;
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
    }
}
