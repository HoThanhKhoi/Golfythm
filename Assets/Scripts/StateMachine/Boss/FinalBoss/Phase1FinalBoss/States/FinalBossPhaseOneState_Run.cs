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

		owner.FaceToPlayer(owner.BossCenter.position);
		owner.MoveToPlayer(owner.RunSpeed);

		if (TimeOut() || owner.GetDistanceToPlayer(owner.BossCenter.position) < owner.AttackRange)
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Combo);
		}
	}
}
