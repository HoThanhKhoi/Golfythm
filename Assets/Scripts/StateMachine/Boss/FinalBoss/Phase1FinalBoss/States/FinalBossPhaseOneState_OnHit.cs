using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_OnHit : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
    public FinalBossPhaseOneState_OnHit(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}
