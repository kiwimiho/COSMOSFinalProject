using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossShoot : State
{
    Boss enemy;
    NavMeshAgent agent;
    StateController controller;
    public float fireRate = 2f;
    public float ballSpeed = 2000f;
    public float lastFire = 0f;

    public override void OnEnter()
    {
        enemy = sc.gameObject.GetComponent<Boss>();
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.speed = 10;
    }

    public override void OnUpdate()
    {
        GameObject player = sc.FindClosestTarget("Player", enemy.shootRange);

        lastFire += Time.deltaTime;

        if(player != null)
        {
            agent.destination = player.transform.position;
        }
        else if (player == null)
        {
            // sc.RemoveTop();
            sc.AddNewState(new BossWait());
        }

        if (lastFire >= fireRate)
        {
            GameObject ball = Object.Instantiate(enemy.prefabPaintball, enemy.muzzle.transform.position, Quaternion.identity);

            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.AddForce(sc.gameObject.transform.forward * ballSpeed);

            lastFire = 0f;
        }

    }
    
    public override void OnExit()
    {
        //This state never exits
    }

}