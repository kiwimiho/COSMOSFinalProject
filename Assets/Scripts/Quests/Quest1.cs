using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest1 : MonoBehaviour
{
    //Making these public so we can watch them change in the designer
    // public GameObject wall;                                 //Wall that we want to lower when the quest is completed
    // public Vector3 hiddenPosition;                         //Calculated hidden position of the wall (not visible)
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public TMP_Text nameText;

    public bool haveItem = false;                           //picked up?                     
    public ExitQuestState state = ExitQuestState.None;      //Current state
    // public bool isNetVisible = false;

    public static bool completed1;
    //Enumeration with all of the states being used for the exit quest
    public enum ExitQuestState
    {
        None, During, Completed
    }

    //The values can be edited in the designer (easy tweaking without recompling code)
    // public float wallSpeed = 1.0f;                  //How fast the wall lowers

    // Start is called before the first frame update
    void Start()
    {
        //Set current state to None (not assigned)
        state = ExitQuestState.None;
        haveItem = false;

        dialoguePanel.SetActive(false);

        //Get the wall game object in the scene
        // wall = GameObject.Find("rockWall");

        // netOnPlayer.SetActive(false);

        //Calculate the wall's hidden position (current position, but subtract 10.5 from the Y axis, which is below ground)
        // hiddenPosition = wall.transform.position - new Vector3(0, 20f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Only lower the wall if the quest has been completed
        // if (state == ExitQuestState.Completed)
        // {
        //     //Lower the wall
        //     // float step = wallSpeed * Time.deltaTime; // calculate distance to move (wall speed times deltaTime as usual)
        //     // wall.transform.position = Vector3.MoveTowards(wall.transform.position, hiddenPosition, step);
        //     Destroy(wall);
        // }
    }

    //Check to see what state the exit quest is in and call the appropriate method
    public void CheckExitQuest()
    {
        switch (state)
        {
            case ExitQuestState.None:
                NoneExitQuest();
                break;
            case ExitQuestState.During:
                DuringExitQuest();
                break;
            case ExitQuestState.Completed:
                CompletedExitQuest();
                break;
        }
    }

    //Currently in the None state, start the quest
    public void NoneExitQuest()
    {
        dialoguePanel.SetActive(true);
        nameText.text = "Geckert";
        dialogueText.text = "Hello, traveler. I have been made weak by the nefarious Colo, who has stolen my artifact. Help me get it back so we can defeat him!";
        // Debug.Log("help me get my artifact over there.");
        state = ExitQuestState.During;
    }

    //Quest is done, nothing more to do
    public void CompletedExitQuest()
    {
        if(Quest1.completed1 && Quest2.completed2 && Quest3.completed3)
        {
            dialoguePanel.SetActive(true);
            nameText.text = "Geckert";
            dialogueText.text = "Thanks for returning all our artifacts! We have brought down the wall for you.";
        }
        else
        {
            dialoguePanel.SetActive(true);
            nameText.text = "Geckert";
            dialogueText.text = "You already brought me my artifact. You should go help the others.";
            // Debug.Log("You already brought me my artifact you should go help the others");
        }
    }

    //Looking for the red block state
    public void DuringExitQuest()
    {
        //Does the player have the red block?
        if (haveItem)
        {
            completed1 = true;
            if(Quest1.completed1 && Quest2.completed2 && Quest3.completed3)
            {
                dialoguePanel.SetActive(true);
                nameText.text = "Geckert";
                dialogueText.text = "Thanks for bringing me my artifact! We have gained enough power to lower the wall for you. Get that Colo!";
            }
            else
            {
                dialoguePanel.SetActive(true);
                nameText.text = "Geckert";
                dialogueText.text = "Thanks for bringing me my artifact! You should go help the others now.";
                //Debug.Log("Thanks for bringing me my artifact, you should go help the others now");
                state = ExitQuestState.Completed;
            }
        }
        else
        {
            //Nope, remind player what's going on
            dialoguePanel.SetActive(true);
            nameText.text = "Geckert";
            dialogueText.text = "My artifact is on the circular path of floating islands. Please bring it to me.";
            //Debug.Log("My artifact is over there, please bring it to me");
        }
    }

    //Player is trying to pick up a block
    public void PickUp(GameObject other)
    {
        //If the player isn't in any of these states, then do nothing. The player will
        //probably push the block around. We could turn off physics on the blocks and turn
        //them into triggers if we don’t want objects to move.
        if(state == ExitQuestState.During)
        {
            if (other.name == "QuestItem1")
            {
                //Yes, pick up block
                dialoguePanel.SetActive(true);
                nameText.text = "";
                dialogueText.text = "You got Geckert's artifact!";
                //Debug.Log("You got a artifact");

                //Pick up green block
                haveItem = true;

                //Remove block
                Destroy(other);
            }
            else
            {
                //Nope, remind player (we could do nothing here also)
                dialoguePanel.SetActive(true);
                nameText.text = "";
                dialogueText.text = "We're looking for a green artifact.";
                //Debug.Log("We're looking for an artifact");
            }
        }
    }

    //Called upon collision with another GameObject (with trigger)
    private void OnTriggerEnter(Collider other)
    {
        //Check to see if player entered the exit quest trigger
        if (other.name == "QuestTrigger1")
        {
            CheckExitQuest();
        }
    }

    //Called upon collision with another GameObject (no trigger)
    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if player ran into one of the blocks
        if (collision.gameObject.name == "QuestItem1")
        {
            PickUp(collision.gameObject);
        }
    }
}

