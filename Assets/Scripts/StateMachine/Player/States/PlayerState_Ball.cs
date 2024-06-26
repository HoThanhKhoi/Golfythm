using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Ball : State<Player, PlayerStateMachine.State>
{
    private GameObject slowZone;
    private Vector2 playerDirectionTowardsGrass;
    private Transform grass;
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
                owner.SetActivePlayerVisual(true, Vector2.up);
                stateMachine.ChangeState(PlayerStateMachine.State.Idle);
            }
        }
        else
        {
            owner.Rb.drag = owner.BallAirDrag;
        }

        if (owner.IsTouchingGrass())
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        GameManager.Instance.TimeManager.SetTimeScale(1f);
        owner.Rb.isKinematic = true;
        owner.Rb.velocity = Vector2.zero;
        owner.Rb.angularVelocity = 0f;
        owner.SetPhysicMaterial(owner.NoBounceMaterial);
        owner.inputReader.SwingEvent -= Bounce;
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        GameManager.Instance.TimeManager.SetTimeScale(1f);

        if (other.collider.CompareTag("Grass"))
        {
            if (slowZone != null)
            {
                playerDirectionTowardsGrass = slowZone.transform.position - other.transform.position;
                owner.SetActivePlayerVisual(true, playerDirectionTowardsGrass);
            }
            stateMachine.ChangeState(PlayerStateMachine.State.Idle);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("SlowZone"))
        {
            GameManager.Instance.TimeManager.SetTimeScale(0.05f);
            slowZone = other.gameObject;
            grass = slowZone.transform.parent;
            owner.inputReader.SwingEvent += Bounce;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.CompareTag("SlowZone"))
        {
            owner.inputReader.SwingEvent -= Bounce;
            GameManager.Instance.TimeManager.SetTimeScale(1f);
        }
    }

    public void Bounce()
    {
        Vector2 velocity = owner.Rb.velocity;
        Vector2 axis = grass.transform.up;

        Vector2 bounceDirection = Vector2.Reflect(velocity, axis);

        owner.Rb.velocity = bounceDirection;
        GameManager.Instance.TimeManager.SetTimeScale(1f);
    }
}