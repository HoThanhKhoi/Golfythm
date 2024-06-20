using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Hurt : State<BossLV1>
{
    public BossLV1State_Hurt(string animBoolName, BossLV1 owner, StateMachine<BossLV1> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
