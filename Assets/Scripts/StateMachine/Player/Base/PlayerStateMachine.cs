using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<Player>
{
    public PlayerStateMachine(Player owner) : base(owner)
    {
        SetUpStateMachine();
    }

    public enum State
    {
        Idle,
        Aim,
        Swing,
        Hit
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new PlayerState_Idle("Idle", owner, this));
        AddState(State.Aim, new PlayerState_Aim("Aim", owner, this));
        AddState(State.Swing, new PlayerState_Swing("Swing", owner, this));
        AddState(State.Hit, new PlayerState_Hit("Hit", owner, this));

        ChangeState(State.Idle);
    }
}