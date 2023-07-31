using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthGUI : MonoBehaviour
{
    TMP_Text healthText;
    // PlayerHealth playerHealth;
    // public int startingHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<TMP_Text>();
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthText();

        if (PlayerHealth.health <= 0)
        {
            Debug.Log("Game over!! Player lost!!");
        }
    }

    void SetHealthText()
    {
        // if(GameOverScreen.died)
        // {
        //     healthText.text = "Health: " + 0;
        // }
        // else
        // {
        //     healthText.text = "Health: " + health;
        // }
        healthText.text = "Health: " + PlayerHealth.health;
    }

    //Static method belongs to the class and doesn't need any instance to call it.
    //Static methods cannot use regular member variables, only static member variables.
    //To call a static method: ClassName.MethodName();
    //For this one: HealthManager.ReduceHealth(1);
    public static void ReduceHealth(int amount)
    {
        PlayerHealth.health -= amount;
    }
}
