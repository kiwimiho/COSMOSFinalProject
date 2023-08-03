using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    StateController controller;
    // public static int numWaypoints = 2;
    public GameObject[] waypoints = new GameObject[2];
    public float viewRange = 20;
    public float shootRange = 40;

    public GameObject prefabPaintball;
    public GameObject muzzle;
    public GameObject winScreen;
    public GameObject bossHealthPanel;
    public TMP_Text bossHealthText;

    // public GameObject dialoguePanel;
    // public TMP_Text tmpText;

    public static int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        // bossHealthPanel = GetComponent("BossHealthPanel");
        bossHealthPanel.SetActive(false);
        // bossHealthText = GameObject.Find("BossHealthText").GetComponent<TMP_Text>();
        winScreen.SetActive(false);
        // tmpText = dialoguePanel.GetComponentInChildren<TMP_Text>();
        // Debug.Log("This is tmpText inside dialogue " + tmpText);
        controller = GetComponent<StateController>();
        health = 5;
        controller.ChangeState(new BossThink());
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            bossHealthPanel.SetActive(false);
            winScreen.SetActive(true);

        }

        bossHealthText.text = "Boss Health: " + health;
    }
}
