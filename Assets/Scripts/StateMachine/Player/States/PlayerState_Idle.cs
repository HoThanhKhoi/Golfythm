using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : State<Player, PlayerStateMachine.State>
{
    public PlayerState_Idle(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.SetActivePlayerVisual(true);

        owner.SetPhysicMaterial(owner.NoBounceMaterial);
        owner.inputReader.AimEvent += ChangeToAimState;
    }

    public override void Update()
    {
        base.Update();
        owner.Rb.velocity = Vector2.zero;

        ReturnSwingForceToMin();

        owner.SpinClub(owner.ClubSpinAngle);
    }

    private void ReturnSwingForceToMin()
    {
        if(owner.SwingForce <= owner.MinSwingForce)
        {
            owner.SwingForce = owner.MinSwingForce;
            return;
        }

        owner.SwingForce -= owner.SwingForce * Time.deltaTime * 2;
    }
    private void ChangeToAimState(bool press)
    {
        if (press)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Aim);
        }
    }

    public override void Exit()
    {
        base.Exit();

        owner.inputReader.AimEvent -= ChangeToAimState;
    }
}
