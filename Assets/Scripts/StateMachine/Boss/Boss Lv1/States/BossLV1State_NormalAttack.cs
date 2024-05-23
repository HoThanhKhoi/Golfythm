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
        Debug.Log("Normal Attack");
        stateMachine.ChangeState(BossLV1_SM.State.NormalAttack);
    }

    public override void Update()
    {
        Debug.Log("Exit Normal Attack");
    }
}
