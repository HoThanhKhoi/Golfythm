using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Ball : State<Player, PlayerStateMachine.State>
{
    public PlayerState_Ball(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.Rb.isKinematic = false;

        if (owner.BounceMaterial != null)
        {
            owner.SetPhysicMaterial(owner.BounceMaterial);
        }


        owner.HitBall(owner.HitDirection, owner.SwingForce);

        owner.SetActivePlayerVisual(false);
        owner.Rb.drag = owner.BallAirDrag;
    }

    public override void Update()
    {
        base.Update();

        if (owner.IsGrounded())
        {
            owner.Rb.drag = owner.BallGroundDrag;

            if (owner.IsStopMoving())
            {
                stateMachine.ChangeState(PlayerStateMachine.State.Idle);
            }
        }
        else
        {
            owner.Rb.drag = owner.BallAirDrag;
        }
    }

    public override void Exit()
    {
        base.Exit();

        owner.Rb.isKinematic = true;
        owner.Rb.velocity = Vector2.zero;
        owner.SetPhysicMaterial(owner.NoBounceMaterial);
        owner.SetActivePlayerVisual(true);
    }
}
