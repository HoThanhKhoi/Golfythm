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
                stateMachine.ChangeState(PlayerStateMachine.State.Destroyed);
                owner.Damage(1);
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
            if (owner.SlowZone != null)
            {
                owner.PlayerDirectionTowardsGrass = owner.SlowZone.transform.position - owner.Grass.transform.position;
                owner.SetActivePlayerVisual(true, owner.PlayerDirectionTowardsGrass);
            }
            stateMachine.ChangeState(PlayerStateMachine.State.Idle);
        }

        if(other.collider.CompareTag("Obstacle"))
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Destroyed);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("SlowZone"))
        {
            GameManager.Instance.TimeManager.SetTimeScale(0.05f);
            owner.SlowZone = other.transform;
            owner.CheckPoint = owner.SlowZone.transform.position;
            owner.Grass = owner.SlowZone.transform.parent;
            owner.inputReader.SwingEvent += Bounce;
            owner.CanSlowTime = true;
        }

        if(other.CompareTag("BossTrigger"))
        {
            GameManager.Instance.StartBossState();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (owner.CanSlowTime)
        {
            if (other.CompareTag("SlowZone"))
            {
                owner.inputReader.SwingEvent -= Bounce;
                GameManager.Instance.TimeManager.SetTimeScale(1f);
            }
        }

    }

    public void Bounce()
    {
        Vector2 velocity = owner.Rb.velocity;
        Vector2 axis = owner.Grass.transform.up;

        Vector2 bounceDirection = Vector2.Reflect(velocity, axis);

        owner.Rb.velocity = bounceDirection;
        GameManager.Instance.TimeManager.SetTimeScale(1f);
    }

    
}