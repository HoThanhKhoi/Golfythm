using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStoneGolemState_Born : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Born(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Update()
    {
        base.Update();

        if(base.IsAnimationFinished())
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.FlyToCenter);
        }
    }
}
