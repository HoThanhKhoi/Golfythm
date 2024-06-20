using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1State_Spell : State<BossLV1, BossLV1_StateMachine.State>
{
    public BossLV1State_Spell(BossLV1 owner, StateMachine<BossLV1, BossLV1_StateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        Debug.Log("Spell");

    }

    public override void Update()
    {
        Debug.Log("Exit Spell");
    }
}
