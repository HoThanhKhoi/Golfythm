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

        owner.StopMoving();
    }

    public override void Update()
    {
        base.Update();

        owner.FaceToPlayer();

        if (TimeOut() && owner.DetectPlayer(owner.BossCenter.position) && owner.GetDistanceToPlayer(owner.BossCenter.position) > owner.AttackRange)
        {
            stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Run);
        }
        else if(TimeOut() && owner.GetDistanceToPlayer(owner.BossCenter.position) <= owner.AttackRange)
        {
            stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Combo);
		}
    }
}
