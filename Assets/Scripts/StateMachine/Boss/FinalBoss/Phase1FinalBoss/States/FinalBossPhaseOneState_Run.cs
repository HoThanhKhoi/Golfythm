using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOneState_Run : State<FinalBossPhaseOne>
{
	public FinalBossPhaseOneState_Run(string animBoolName, FinalBossPhaseOne owner, StateMachine<FinalBossPhaseOne> stateMachine) : base(animBoolName, owner, stateMachine)
	{
	}
}
