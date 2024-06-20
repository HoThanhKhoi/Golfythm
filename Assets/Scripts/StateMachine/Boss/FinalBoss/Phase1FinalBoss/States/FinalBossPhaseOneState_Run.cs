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

		Debug.Log("Boss Idle");
		stateTimer = 1f;
	}

	public override void Update()
	{
		base.Update();

		if (stateTimer < 0)
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Run);
		}
	}
}
