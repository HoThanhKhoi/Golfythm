using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_Move : State<Ball>
{
    public BallState_Move(string animBoolName, Ball owner, StateMachine<Ball> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();

        if(owner.Rb.velocity.magnitude <= 0.1f)
        {
            stateMachine.ChangeState(BallStateMachine.State.Stay);
        }
    }
}
