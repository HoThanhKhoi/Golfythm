using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Hit : State<Player>
{
    private float stateTimer;
    public PlayerState_Hit(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.SetUpBall();
        owner.CameraFollowBall();

        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;
        if(stateTimer <= 0)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Idle);
        }
    }
}
