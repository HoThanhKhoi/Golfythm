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
        AddState(State.Idle, new FinalBossPhaseOneState_Idle(owner, this, anim));
        AddState(State.Run, new FinalBossPhaseOneState_Run(owner, this, anim));
		AddState(State.Dash, new FinalBossPhaseOneState_Dash(owner, this, anim));
		AddState(State.Jump, new FinalBossPhaseOneState_Jump(owner, this, anim));
		AddState(State.Fall, new FinalBossPhaseOneState_Fall(owner, this, anim));
		AddState(State.Shield_Crash_Startup, new FinalBossPhaseOneState_ShieldCrashStartup(owner, this, anim));
		AddState(State.Shield_Crash_Landing, new FinalBossPhaseOneState_ShieldCrashLanding(owner, this, anim));
		AddState(State.AirCombo, new FinalBossPhaseOneState_AirCombo(owner, this, anim));
		AddState(State.Combo, new FinalBossPhaseOneState_Combo(owner, this, anim));
		AddState(State.Block, new FinalBossPhaseOneState_Block(owner, this, anim));
		AddState(State.On_Hit, new FinalBossPhaseOneState_OnHit(owner, this, anim));
		AddState(State.Death, new FinalBossPhaseOneState_Death(owner, this, anim));

		Debug.Log("Set up");

        ChangeState(State.Idle);
    }
}
