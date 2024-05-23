using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Idle : State<BossLV1>
{
    public BossLV1State_Idle(string animBoolName, BossLV1 owner, StateMachine<BossLV1> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        stateMachine.ChangeState(BossLV1_SM.State.Idle);
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
    }
}
