using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Death : State<BossLV1, BossLV1_StateMachine.State>
{
    public BossLV1State_Death(BossLV1 owner, StateMachine<BossLV1, BossLV1_StateMachine.State> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Update()
    {
        Debug.Log("Exit Death");
    }
}
