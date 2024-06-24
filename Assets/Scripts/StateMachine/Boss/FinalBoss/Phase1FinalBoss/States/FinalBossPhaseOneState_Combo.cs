using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBossPhaseOneState_Combo : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
	public FinalBossPhaseOneState_Combo(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
	{
	}

	public override void Enter()
	{
		base.Enter();

		stateTimer = animationLength;
	}

	public override void Update()
	{
		base.Update();

		if (TimeOut())
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Idle);
		}
	}
}
