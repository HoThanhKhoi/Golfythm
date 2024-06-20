using System.Collections;
using UnityEngine;

public class BossStoneGolemState_FlyToCenter : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public BossStoneGolemState_FlyToCenter(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        MoveToCenter();
    }

    public override void Update()
    {
        base.Update();

        owner.FaceTo(owner.CenterTransform.position);

        if(owner.GetDistanceToPosition(owner.CenterTransform.position) < 0.001f)
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.Idle);
        }
        
    }

    public override void Exit()
    {
        base.Exit();

        owner.Rb.velocity = Vector2.zero;
    }

    private void MoveToCenter()
    {
        owner.MoveToPosition(owner.CenterTransform.position, owner.FlySpeed);
    }
}
