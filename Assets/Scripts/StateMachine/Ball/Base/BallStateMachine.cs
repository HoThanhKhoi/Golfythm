using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStateMachine : StateMachine<Ball, BallStateMachine.State>
{
    public enum State
    {
        Stay,
        Move,
        DecreaseBounciness
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Stay, new BallState_Stay(owner, this, anim));
        AddState(State.Move, new BallState_Move(owner, this, anim));
        AddState(State.DecreaseBounciness, new BallState_DecreaseBounciness(owner, this, anim));

        ChangeState(State.Stay);
    }
}
