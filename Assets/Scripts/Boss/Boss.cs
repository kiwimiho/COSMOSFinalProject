using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    StateController controller;
    // public static int numWaypoints = 2;
    public GameObject[] waypoints = new GameObject[2];
    public float viewRange = 20;
    public float shootRange = 40;

    public GameObject prefabPaintball;
    public GameObject muzzle;

    public static int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new BossThink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
