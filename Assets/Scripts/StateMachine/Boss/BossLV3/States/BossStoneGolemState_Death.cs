using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Death : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Death(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}