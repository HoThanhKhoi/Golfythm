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

		if (owner.GetDistanceToPlayer(owner.BossCenter.position) < owner.AttackRange)
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Combo);
		}
		else if(TimeOut())
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
		}
	}
}
