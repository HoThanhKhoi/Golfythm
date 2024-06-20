using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_Move : State<Ball, BallStateMachine.State>
{
    public BallState_Move(Ball owner, StateMachine<Ball, BallStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        Debug.Log("Decrease");

        owner.DecreaseBounciness();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
