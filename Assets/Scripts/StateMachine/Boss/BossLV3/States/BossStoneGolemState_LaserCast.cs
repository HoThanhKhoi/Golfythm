using System.Collections;
using UnityEngine;


public class BossStoneGolemState_LaserCast : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_LaserCast(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = animationLength * 20;

        owner.CastLaser();
    }

    public override void Update()
    {
        base.Update();

        owner.PointLaserToPlayer();

        if(TimeOut())
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.LaserShoot);
        }
    }
}
