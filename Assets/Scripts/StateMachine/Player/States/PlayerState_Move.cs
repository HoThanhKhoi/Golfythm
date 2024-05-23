using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : State<Player>
{
    public PlayerState_Move(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Move");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
