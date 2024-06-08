using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Zip : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    float velocityScale;
    float delay;
    public BossStoneGolemState_Zip(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = owner.ZipShootCooldown - animationLength;
        delay = 1f;
        velocityScale = 1;
    }

    public override void Update()
    {
        base.Update();

        delay -= Time.deltaTime;

        owner.FaceToPlayer();

        if (TimeOut())
        {
            if (!owner.IsZipShootCountFull())
            {
                stateTimer = owner.ZipShootCooldown;
                owner.SetActiveZipIndicator(false);
                owner.ShootSelfToPlayer();
                velocityScale = 1;
            }
        }

        if (owner.Rb.velocity.magnitude <= 2f && delay <= 0)
        {
            if (owner.IsZipShootCountFull())
            {
                owner.SetActiveZipIndicator(false);
                stateMachine.ChangeState(BossStoneGolemStateMachine.State.FlyToCenter);
            }
            else
            {
                owner.SetActiveZipIndicator(true);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        velocityScale -= Time.deltaTime * 0.01f;

        owner.Rb.velocity *= velocityScale;
    }

    override public void Exit()
    {
        base.Exit();

        owner.StopMoving();
    }
}