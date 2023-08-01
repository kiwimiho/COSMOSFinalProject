using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    StateController controller;
    // public static int numWaypoints = 2;
    public GameObject[] waypoints = new GameObject[2];
    public float viewRange = 20;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new EnemyThink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
