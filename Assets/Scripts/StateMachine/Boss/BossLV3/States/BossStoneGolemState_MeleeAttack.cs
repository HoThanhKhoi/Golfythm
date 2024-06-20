using System.Collections;
using UnityEngine;

public class BossStoneGolemState_MeleeAttack : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_MeleeAttack(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}