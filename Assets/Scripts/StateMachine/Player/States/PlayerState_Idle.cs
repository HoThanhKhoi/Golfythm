using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : State<Player>
{
    public PlayerState_Idle(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }
}
