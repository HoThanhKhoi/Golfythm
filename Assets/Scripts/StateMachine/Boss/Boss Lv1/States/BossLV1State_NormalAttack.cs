using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_NormalAttack : State<BossLV1>
{
    public BossLV1State_NormalAttack(string animBoolName, BossLV1 owner, StateMachine<BossLV1> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();

        if (TimeOut())
        {
            stateMachine.ChangeState(BossLV1_StateMachine.State.Idle);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
