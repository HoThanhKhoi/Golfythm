using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneStateMachine : StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
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
        AddState(State.Idle, new FinalBossPhaseOneState_Idle(owner, this));
        AddState(State.Run, new FinalBossPhaseOneState_Run(owner, this));

        Debug.Log("Set up");

        ChangeState(State.Idle);
    }
}
