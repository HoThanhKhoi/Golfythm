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
        AddState(State.Stay, new BallState_Stay(owner, this));
        AddState(State.Move, new BallState_Move(owner, this));
        AddState(State.DecreaseBounciness, new BallState_DecreaseBounciness(owner, this));

        ChangeState(State.Stay);
    }
}
