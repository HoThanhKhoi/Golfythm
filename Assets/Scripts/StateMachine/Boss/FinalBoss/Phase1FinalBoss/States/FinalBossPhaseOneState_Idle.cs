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

        if (TimeOut())
        {
            Debug.Log("Ngu");
            stateMachine.ChangeState(FinalBossPhaseOneStateMachine.State.Run);
        }
    }
}
