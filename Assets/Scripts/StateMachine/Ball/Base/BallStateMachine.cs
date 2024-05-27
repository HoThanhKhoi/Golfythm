using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStateMachine : StateMachine<Ball>
{
    public enum State
    {
        Stay,
        Move
    }

    public BallStateMachine(Ball owner) : base(owner)
    {
        SetUpStateMachine();
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Stay, new BallState_Stay("Stay", owner, this));
        AddState(State.Move, new BallState_Move("Move", owner, this));

        ChangeState(State.Stay);
    }
}
