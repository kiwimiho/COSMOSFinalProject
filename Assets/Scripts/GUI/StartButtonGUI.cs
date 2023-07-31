using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonGUI : MonoBehaviour
{
    public GameObject startScreen;
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
    }
}
