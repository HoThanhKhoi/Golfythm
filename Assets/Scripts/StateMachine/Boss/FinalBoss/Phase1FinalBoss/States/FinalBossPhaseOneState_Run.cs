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

		
	}

	public override void Update()
	{
		base.Update();

		owner.MoveToPlayer(owner.RunSpeed);

		Debug.Log(owner.GetDistanceToPlayer());

		if (TimeOut() || owner.GetDistanceToPlayer() <= owner.AttackRange)
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
		}
	}
}
