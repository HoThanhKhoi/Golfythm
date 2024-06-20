using System.Collections;
using UnityEngine;


public class BossStoneGolemState_LaserCast : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_LaserCast(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

}
