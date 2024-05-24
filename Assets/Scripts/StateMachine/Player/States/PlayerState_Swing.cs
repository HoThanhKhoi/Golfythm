using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Swing : State<Player>
{
    public PlayerState_Swing(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.SpinClub();
    }
}
