using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_ShieldCrashLanding : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
    public FinalBossPhaseOneState_ShieldCrashLanding(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

	public override void Enter()
	{
		base.Enter();

		stateTimer = animationLength;

		owner.Rb.gravityScale = 1;
		owner.StopMoving();
	}

	public override void Update()
	{
		base.Update();

		if(TimeOut())
		{	
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
		}
	}
}
