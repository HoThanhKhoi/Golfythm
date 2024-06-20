using System.Collections;
using UnityEngine;


public class BossStoneGolemState_ArmorBuff : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_ArmorBuff(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = animationLength;
    }

    public override void Update()
    {
        base.Update();

        if(TimeOut())
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.Zip);
        }
    }
}
