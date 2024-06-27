using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_Run : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
	public FinalBossPhaseOneState_Run(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
	{
	}

	public override void Enter()
	{
		base.Enter();

		stateTimer = owner.RunDuration;

		
		owner.Rb.gravityScale = 1;
	}

	public override void Update()
	{
		base.Update();

		owner.FaceToPlayer(owner.BossCenter.position);

		owner.Rb.velocity = new Vector2(owner.RunSpeed * owner.transform.right.x, owner.Rb.velocity.y);

		if (owner.GetPlayerPosition().y <= (owner.BossCenter.position.y + 4f))
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
		else if(owner.GetPlayerPosition().y > (owner.BossCenter.position.y + 4f))
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
		}

	}
}
