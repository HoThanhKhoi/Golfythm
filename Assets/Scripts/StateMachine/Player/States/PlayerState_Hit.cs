using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Hit : State<Player, PlayerStateMachine.State>
{
    public PlayerState_Hit(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.SetUpBall();
        owner.CameraFollowBall();
    }
}
