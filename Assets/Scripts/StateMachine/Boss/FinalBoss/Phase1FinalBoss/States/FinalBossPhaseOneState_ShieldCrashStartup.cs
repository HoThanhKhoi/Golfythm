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
	}

	override public void Update()
	{
		base.Update();

		if (owner.IsOnGround(owner.BossCenter.position))
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Landing);
		}
	}

}
