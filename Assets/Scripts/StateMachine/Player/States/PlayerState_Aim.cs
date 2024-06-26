using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Aim : State<Player, PlayerStateMachine.State>
{
    private float direction = 1f;
    private float initialRightTransformX;
    public PlayerState_Aim(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.inputReader.AimEvent += ChangeToIdleState;
        owner.inputReader.SwingEvent += ChangeToSwingState;

        initialRightTransformX = owner.PlayerVisual.transform.right.x;

        owner.DotsActive(true);
    }

    public override void Update()
    {
        base.Update();
        bool flip = owner.inputReader.AimDirection.x < 0;

        BounceSwingForceBetweenMaxAndMin();

        owner.SpinClub(owner.ClubSpinAngle);

        owner.SetUpDotsPosition();

        if (owner.PlayerVisual != null)
        {
            owner.FlipPlayerVisual(flip, initialRightTransformX);
        }
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

        owner.DotsActive(false);
    }
}
