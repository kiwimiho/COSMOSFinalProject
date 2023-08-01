using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject wall;              
    public Vector3 hiddenPosition;
    public float wallSpeed = 5.0f; 
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("Wall");

        //Calculate the wall's hidden position (current position, but subtract from the Y axis, which is below ground)
        hiddenPosition = wall.transform.position - new Vector3(0, 50f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Quest1.completed1 && Quest2.completed2 && Quest3.completed3)
        {
            // dialoguePanel.SetActive(true);
            // dialogueText.text = "Thanks for returning all our artifacts! We have brought down the wall for you.";

            float step = wallSpeed * Time.deltaTime;
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, hiddenPosition, step);
        }
    }
}
