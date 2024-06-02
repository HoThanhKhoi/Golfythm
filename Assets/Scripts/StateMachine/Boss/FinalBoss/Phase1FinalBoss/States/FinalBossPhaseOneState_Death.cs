using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_Death : State<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State>
{
    public FinalBossPhaseOneState_Death(FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne, FinalBossPhaseOneStateMachine.State> stateMachine) : base(owner, stateMachine)
    {
    }
}
