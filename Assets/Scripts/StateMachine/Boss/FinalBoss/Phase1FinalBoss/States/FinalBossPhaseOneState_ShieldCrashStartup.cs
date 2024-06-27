using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_ShieldCrashStartup : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
    public FinalBossPhaseOneState_ShieldCrashStartup(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

	public override void Enter()
	{
		base.Enter();

		stateTimer = animationLength;

		owner.Rb.gravityScale = 1;
	}

	override public void Update()
	{
		base.Update();

		Debug.Log("Shield Crash Startup");

		if(TimeOut())
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crashing);
		}
	}

}
