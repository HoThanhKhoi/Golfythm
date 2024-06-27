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

		owner.Rb.gravityScale = 0;

		owner.MoveToPlayerHorizontal(owner.BossCenter.position, owner.DashSpeed);
	}

	public override void Update()
	{
		base.Update();

        Debug.Log("Dash");

		if (owner.IsOnGround(owner.BossCenter.position))
		{
			Debug.Log("Is on ground");
			if (owner.GetDistanceToPlayer(owner.BossCenter.position) < owner.AttackRange)
			{
				Debug.Log("Combo");
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Combo);
			}
			else if (TimeOut())
			{
				Debug.Log("On ground time out");
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
			}
		}
		else if(!owner.IsOnGround(owner.BossCenter.position))
		{
			Debug.Log("Is on air");
			if (owner.GetDistanceToPlayer(owner.BossCenter.position) < owner.AttackRange)
			{
				Debug.Log("Air Combo");
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.AirCombo);
			}
			else if (TimeOut())
			{
				Debug.Log("On air time out");
				owner.StopMoving();
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Startup);
			}
			else if (Mathf.Abs(owner.BossCenter.position.x - owner.GetPlayerPosition().x) <= owner.Tolerance)
			{
				owner.StopMoving();
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Startup);
			}
		}	

	}
}
