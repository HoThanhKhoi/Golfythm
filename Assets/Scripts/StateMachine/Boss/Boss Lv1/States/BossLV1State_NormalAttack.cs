using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_NormalAttack : State<BossLV1, BossLV1_StateMachine.State>
{
    public BossLV1State_NormalAttack(BossLV1 owner, StateMachine<BossLV1, BossLV1_StateMachine.State> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 2f;
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
