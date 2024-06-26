using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_Idle : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
	public FinalBossPhaseOneState_Idle(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
	{
	}

	public override void Enter()
	{
		base.Enter();

		stateTimer = owner.IdleDuration;

		owner.Rb.gravityScale = 1;

		owner.StopMoving();
	}

	public override void Update()
	{
		base.Update();

		owner.FaceToPlayer(owner.BossCenter.position);

		if (TimeOut())
		{
			if(owner.GetPlayerPosition().y <= (owner.BossCenter.position.y + 4f) 
				&& owner.GetPlayerPosition().y >= (owner.BossCenter.position.y - 4f))
			{
				if (owner.GetDistanceToPlayer(owner.BossCenter.position) > owner.AttackRange)
				{
					if (owner.RunCounter > 0)
					{
						owner.DecreaseRunCounter();
						stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Run);
					}
					else if (owner.RunCounter == 0)
					{
						owner.ResetRunCounter();
						stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Dash);
					}
				}
				else if (owner.GetDistanceToPlayer(owner.BossCenter.position) <= owner.AttackRange)
				{
					stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Combo);
				}
			}
			else if (owner.GetPlayerPosition().y > (owner.BossCenter.position.y + 4f))
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Jump);
			}
			else if (owner.GetPlayerPosition().y < (owner.BossCenter.position.y - 4f))
			{
				stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Dash);
			}
		}
		
	}
}
