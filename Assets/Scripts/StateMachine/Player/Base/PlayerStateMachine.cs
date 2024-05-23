using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<Player>
{
    public enum State
    {
        Idle,
        Move
    }
    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new PlayerState_Idle("Idle", owner, this));
        AddState(State.Move, new PlayerState_Move("Move", owner, this));

        ChangeState(State.Idle);
    }
}