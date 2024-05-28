using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Death : State<BossLV1>
{
    public BossLV1State_Death(string animBoolName, BossLV1 owner, StateMachine<BossLV1> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Death");
        stateMachine.ChangeState(BossLV1_StateMachine.State.Death);
    }

    public override void Update()
    {
        Debug.Log("Exit Death");
    }
}
