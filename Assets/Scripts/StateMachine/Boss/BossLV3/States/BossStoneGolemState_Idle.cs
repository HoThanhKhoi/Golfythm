using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Idle : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Idle(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 2f;
    }

    public override void Update()
    {
        base.Update();

        owner.FaceToPlayer();

        if (TimeOut())
        {
            if (!owner.IsProjectileCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.RangeAttack);
            }
            else if(!owner.IsZipShootCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.Zip);
            }
            else if(!owner.IsLaserCastCountFull())
            {
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.Glowing);
            }
        }
    }
}
