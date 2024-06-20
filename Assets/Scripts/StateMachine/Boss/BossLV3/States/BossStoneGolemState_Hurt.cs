using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Hurt : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Hurt(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}