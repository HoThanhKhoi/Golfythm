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
	}

	public override void Update()
	{
		base.Update();

		

		if (owner.GetPlayerPosition().y == owner.BossCenter.position.y)
		{
			if (owner.GetDistanceToPlayer(owner.BossCenter.position) <= owner.AttackRange)
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.AirCombo);
			}
			else if (owner.GetDistanceToPlayer(owner.BossCenter.position) > owner.AttackRange)
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Dash);
			}
		}
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		owner.FaceToPlayer();
		owner.MoveToPlayerVertical(owner.BossCenter.position, owner.JumpSpeed);
	}
}
