using System.Collections;
using UnityEngine;


public class BossStoneGolemState_LaserShoot : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_LaserShoot(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.LaserStartShoot();
        stateTimer = owner.LaserShootTime;
    }

    public override void Update()
    {
        base.Update();

        owner.ShootingLaser();

        if(TimeOut())
        {
            owner.EndShootingLaser();
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.Glowing);
        }
    }
}
