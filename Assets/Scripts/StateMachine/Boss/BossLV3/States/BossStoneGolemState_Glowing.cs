﻿using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Glowing : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Glowing(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}
