using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_AirCombo : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
    public FinalBossPhaseOneState_AirCombo(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

	override public void Enter()
    {
        base.Enter();

		stateTimer = animationLength;

		owner.Rb.gravityScale = 0;

		owner.StopMoving();
	}

	public override void Update()
	{
		base.Update();

		if (TimeOut())
		{
			stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Shield_Crash_Startup);
		}
	}
}
