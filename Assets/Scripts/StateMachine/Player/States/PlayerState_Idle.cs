using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : State<Player>
{
    public PlayerState_Idle(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        stateMachine.ChangeState(PlayerStateMachine.State.Move);
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
    }
}
