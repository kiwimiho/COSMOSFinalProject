using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : State
{
    Enemy enemy;
    public int waypointIndex = 0;
    NavMeshAgent agent;

    //When the state starts for the first time
    public override void OnEnter()
    {
        enemy = sc.gameObject.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.speed = 3;

        //Find nearest waypoint - walk to it
        GameObject waypoint = sc.FindClosestTarget("Waypoint", Mathf.Infinity);
        waypointIndex = Array.IndexOf(enemy.waypoints, waypoint);

        agent.destination = waypoint.transform.position;
    }

    //Called during Update()
    public override void OnUpdate()
    {
        GameObject player = sc.FindClosestTarget("Player", enemy.viewRange);

        if(player != null)
        {
            sc.AddNewState(new EnemyAttack());
        }
    }

    //When state is turned off
    public override void OnExit()
    {

    }

    //When the object hits a trigger (or is a trigger)
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            waypointIndex++;
            if (waypointIndex >= enemy.waypoints.Length) { waypointIndex = 0; }
            agent.destination = enemy.waypoints[waypointIndex].transform.position;
        }
    }

    //When the object touches a RigidBody
    public override void OnCollisionEnter(Collision collision)
    {

    }
}
