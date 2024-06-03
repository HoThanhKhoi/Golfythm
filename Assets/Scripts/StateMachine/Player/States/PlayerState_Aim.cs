using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Aim : State<Player, PlayerStateMachine.State>
{
    private float direction = 1f;

    public PlayerState_Aim(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.inputReader.AimEvent += ChangeToIdleState;
        owner.inputReader.SwingEvent += ChangeToSwingState;

        owner.DotsActive(true);

        Debug.Log("Aim");
    }

    public override void Update()
    {
        base.Update();

        BounceSwingForceBetweenMaxAndMin();

        owner.SpinClub(owner.ClubSpinAngle);

        owner.SetUpDotsPosition();
    }

    private void BounceSwingForceBetweenMaxAndMin()
    {
        owner.SwingForce += direction * owner.SwingForceSpeed * Time.deltaTime;

        if (owner.SwingForce >= owner.MaxSwingForce)
        {
            owner.SwingForce = owner.MaxSwingForce;
            direction = -1f;
        }
        else if (owner.SwingForce <= owner.MinSwingForce)
        {
            owner.SwingForce = owner.MinSwingForce;
            direction = 1f;
        }
    }

    private void ChangeToSwingState()
    {
        owner.HitDirection = owner.inputReader.AimDirection;
        stateMachine.ChangeState(PlayerStateMachine.State.Swing);
    }

    private void ChangeToIdleState(bool press)
    {
        if (!press)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        owner.inputReader.AimEvent -= ChangeToIdleState;
        owner.inputReader.SwingEvent -= ChangeToSwingState;

        //owner.DotsActive(false);
    }
}
