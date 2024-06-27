using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_Jump : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
	public FinalBossPhaseOneState_Jump(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
	{
	}

	public override void Enter()
	{
		base.Enter();

		stateTimer = owner.JumpDuration;
	}

	public override void Update()
	{
		base.Update();

		owner.MoveToPlayerVertical(owner.BossCenter.position, owner.JumpSpeed);
		Debug.Log("jumping");

		if (Mathf.Abs(owner.BossCenter.position.y - (owner.GetPlayerPosition().y + 1f)) <= owner.Tolerance)
		{
			Debug.Log("player.y = boss.y");
			owner.FaceToPlayer();
			if (owner.GetDistanceToPlayer(owner.BossCenter.position) <= owner.AttackRange)
			{
				Debug.Log("Player is close to the boss");
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.AirCombo);
			}
			else if (owner.GetDistanceToPlayer(owner.BossCenter.position) > owner.AttackRange)
			{
				Debug.Log("Player is not close to the boss");
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Dash);
			}
		}
		else if(TimeOut())
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Startup);
		}
	}
}
