using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Spell : State<BossLV1>
{
    public BossLV1State_Spell(string animBoolName, BossLV1 owner, StateMachine<BossLV1> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Spell");
        stateMachine.ChangeState(BossLV1_StateMachine.State.Spell);
    }

    public override void Update()
    {
        Debug.Log("Exit Spell");
    }
}
