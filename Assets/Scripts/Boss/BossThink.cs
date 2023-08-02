using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThink : State
{
    Boss enemy;

    public override void OnEnter()
    {
        doNotRemove = true;
        enemy = sc.gameObject.GetComponent<Boss>();
    }

    public override void OnUpdate()
    {
        //What does the guard do?

        //Just patrol
        sc.AddNewState(new BossWait());
    }
    
    public override void OnExit()
    {
        //This state never exits
    }
}
