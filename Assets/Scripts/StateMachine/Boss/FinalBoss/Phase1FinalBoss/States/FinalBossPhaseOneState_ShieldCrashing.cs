using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_ShieldCrashing : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
	public FinalBossPhaseOneState_ShieldCrashing(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
	{
	}

	override public void Enter()
	{
		base.Enter();

		owner.Rb.gravityScale = 1;

		stateTimer = owner.ShieldCrashDuration;
	}

	override public void Update()
	{
		base.Update();

		Debug.Log("Shield Crashing");

		owner.Rb.velocity = Vector2.down * owner.ShieldCrashSpeed;

		if (owner.IsOnGround(owner.BossCenter.position) || TimeOut())
		{
			Debug.Log("is on ground after crashing");
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Landing);
		}
	}
}
