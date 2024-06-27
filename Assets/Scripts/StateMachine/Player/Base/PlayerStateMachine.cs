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
        Ball,
        Destroyed
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Idle, new PlayerState_Idle(owner, this, anim));
        AddState(State.Aim, new PlayerState_Aim(owner, this, anim));
        AddState(State.Swing, new PlayerState_Swing(owner, this, anim));
        AddState(State.Ball, new PlayerState_Ball(owner, this, anim));
        AddState(State.Destroyed, new PlayerState_Destroyed(owner, this, anim));

    }

    private void Start()
    {
        ChangeState(State.Ball);
    }
}