using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Imune : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Imune(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }
}