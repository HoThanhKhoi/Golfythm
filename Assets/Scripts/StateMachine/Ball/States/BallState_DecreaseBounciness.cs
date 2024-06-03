using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_DecreaseBounciness : State<Ball, BallStateMachine.State>
{
    public BallState_DecreaseBounciness(Ball owner, StateMachine<Ball, BallStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("SlowDown Enter");
    }

    public override void Update()
    {
        base.Update();

        if (owner.Rb.velocity.magnitude <= 0.1f)
        {
            stateMachine.ChangeState(BallStateMachine.State.Stay);
        }
    }
}
