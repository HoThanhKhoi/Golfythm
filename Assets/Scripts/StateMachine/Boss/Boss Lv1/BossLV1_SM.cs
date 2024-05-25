using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1_SM : StateMachine<BossLV1>
{
    public BossLV1_SM(BossLV1 owner) : base(owner)
    {
    }

    public enum State
    {
        Idle,
        Walk,
        NormalAttack
    }
    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new BossLV1State_Idle("Idle", owner, this));
        AddState(State.Walk, new BossLV1State_Walk("Walk", owner, this));
        AddState(State.NormalAttack, new BossLV1State_NormalAttack("NormalAttack", owner, this));

        ChangeState(State.Idle);
    }
}
