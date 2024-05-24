using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : State<Player>
{
    public PlayerState_Idle(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
        owner.inputReader.AimEvent += EnterAim;
    }

    private void EnterAim(bool press)
    {
        if(press)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Aim);
        }
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Idle");
    }
}
