using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<Player, PlayerStateMachine.State>
{
    public enum State
    {
        Idle,
        Aim,
        Swing,
        Hit
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new PlayerState_Idle(owner, this));
        AddState(State.Aim, new PlayerState_Aim(owner, this));
        AddState(State.Swing, new PlayerState_Swing(owner, this));
        AddState(State.Hit, new PlayerState_Hit(owner, this));

        ChangeState(State.Idle);
    }
}