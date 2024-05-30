using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneStateMachine : StateMachine<FinalBossPhaseOne>
{
    public FinalBossPhaseOneStateMachine(FinalBossPhaseOne owner) : base(owner)
    {
        SetUpStateMachine();
    }

    public enum State
    {
        Idle,
        Run,
        Dash,
        Jump,
        Fall,
        Shield_Crash_Startup,
        Shield_Crash_Landing,
        AirCombo,
        Combo,
        Block,
        On_Hit,
        Death
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new PlayerState_Idle("Idle", owner, this));
        

        ChangeState(State.Idle);
    }
}
