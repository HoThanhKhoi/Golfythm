using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Hit : State<Player>
{
    public PlayerState_Hit(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.SetUpBall();
        owner.CameraFollowBall();
    }
}
