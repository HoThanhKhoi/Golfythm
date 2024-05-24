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
        Swing
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new PlayerState_Idle("Idle", owner, this));
        AddState(State.Aim, new PlayerState_Aim("Aim", owner, this));
        AddState(State.Swing, new PlayerState_Swing("Swing", owner, this));

        ChangeState(State.Idle);
    }
}