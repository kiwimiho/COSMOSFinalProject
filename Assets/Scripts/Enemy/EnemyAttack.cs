using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : State
{
    Enemy enemy;
    NavMeshAgent agent;
    StateController controller;

    public override void OnEnter()
    {
        enemy = sc.gameObject.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.speed = 10;
    }

    public override void OnUpdate()
    {
        GameObject player = sc.FindClosestTarget("Player", enemy.viewRange);

        if(player != null)
        {
            agent.destination = player.transform.position;
        }
        else if (player == null)
        {
            // sc.RemoveTop();
            sc.AddNewState(new EnemyPatrol());
        }

    }
    
    public override void OnExit()
    {
        //This state never exits
    }

}