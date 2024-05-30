using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1_StateMachine : StateMachine<BossLV1>
{
    public BossLV1_StateMachine(BossLV1 owner) : base(owner)
    {
        SetUpStateMachine();
    }

    public enum State
    {
        Idle,
        Walk,
        NormalAttack,
        Cast,
        Spell,
        Hurt,
        Death
    }
    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new BossLV1State_Idle("Idle", owner, this));
        AddState(State.Walk, new BossLV1State_Walk("Walk", owner, this));
        AddState(State.NormalAttack, new BossLV1State_NormalAttack("Normal Attack", owner, this));
        AddState(State.Cast, new BossLV1State_Cast("Cast", owner, this));
        AddState(State.Spell, new BossLV1State_Spell("Spell", owner, this));
        AddState(State.Hurt, new BossLV1State_Hurt("Hurt", owner, this));
        AddState(State.Death, new BossLV1State_Death("Death", owner, this));

        ChangeState(State.Idle);
    }
}
