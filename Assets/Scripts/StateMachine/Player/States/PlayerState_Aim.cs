using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Aim : State<Player>
{
    public PlayerState_Aim(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
        owner.inputReader.AimEvent += ExitAim;
    }

    private void ExitAim(bool press)
    {
        if(!press)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Idle);
        }
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Aim");
    }
}
