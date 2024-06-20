using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1_StateMachine : StateMachine<BossLV1, BossLV1_StateMachine.State>
{
    public enum State
    {
        Idle,
        Walk,
        NormalAttack
    }
    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new BossLV1State_Idle(owner, this, anim));
        AddState(State.Walk, new BossLV1State_Walk(owner, this, anim));
        AddState(State.NormalAttack, new BossLV1State_NormalAttack(owner, this, anim));

        ChangeState(State.Idle);
    }
}
