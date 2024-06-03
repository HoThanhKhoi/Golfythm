using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_Stay : State<Ball, BallStateMachine.State>
{
    public BallState_Stay(Ball owner, StateMachine<Ball, BallStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Stay");

        owner.Rb.velocity = Vector3.zero;
    }
}
