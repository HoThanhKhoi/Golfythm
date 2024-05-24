using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Aim : State<Player>
{
    public PlayerState_Aim(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.SpinClub();
    }
}
