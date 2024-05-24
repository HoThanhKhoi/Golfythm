using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Walk : State<BossLV1>
{
    public BossLV1State_Walk(string animBoolName, BossLV1 owner, StateMachine<BossLV1> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Walk");
        stateMachine.ChangeState(BossLV1_SM.State.Walk);
    }

    public override void Exit()
    {
        Debug.Log("Exit Walk");
    }
}
