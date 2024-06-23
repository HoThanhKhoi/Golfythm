using UnityEngine;

public class BossStoneGolemState_Rest : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_Rest(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = owner.RestTime;
        owner.ResetAttackCount();
        owner.Rb.velocity = Vector2.down * owner.RestFallSpeed;
    }

    public override void Update()
    {
        base.Update();

        if(TimeOut())
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.FlyToCenter);
        }
    }

    public override void Exit()
    {
        base.Exit();

        owner.Rb.velocity = Vector2.zero;
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        if(other.gameObject.CompareTag("Player"))
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.Hurt);
        }
    }
}