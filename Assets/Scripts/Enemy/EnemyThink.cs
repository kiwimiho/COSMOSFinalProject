using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThink : State
{
    Enemy enemy;

    public override void OnEnter()
    {
        doNotRemove = true;
        enemy = sc.gameObject.GetComponent<Enemy>();
    }

    public override void OnUpdate()
    {
        //What does the guard do?

        //Just patrol
        sc.AddNewState(new EnemyPatrol());
    }
    
    public override void OnExit()
    {
        //This state never exits
    }
}
