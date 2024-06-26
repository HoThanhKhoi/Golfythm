using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class FinalBossPhaseOneState_Dash : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
	public FinalBossPhaseOneState_Dash(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
	{
	}

	public override void Enter()
	{
		base.Enter();

		stateTimer = owner.DashDuration;
	}

	public override void Update()
	{
		base.Update();

		owner.MoveToPlayerHorizontal(owner.BossCenter.position, owner.RunSpeed);
		if (owner.IsOnGround(owner.BossCenter.position))
		{
			if (owner.GetDistanceToPlayer(owner.BossCenter.position) < owner.AttackRange)
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Combo);
			}
			else if (TimeOut())
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
			}
		}
		else if(!owner.IsOnGround(owner.BossCenter.position))
		{
			if (owner.GetDistanceToPlayer(owner.BossCenter.position) < owner.AttackRange)
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.AirCombo);
			}
			else if (TimeOut())
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Startup);
			}
		}	

	}
}
