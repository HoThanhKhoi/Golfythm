using System.Collections;
using UnityEngine;

public class BossStoneGolemState_RangeAttack : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_RangeAttack(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}