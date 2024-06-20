using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : StateOwner
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    private Player player;
    private Vector2 playerOffset;

    private Vector2 stayPosition;

    private BallStateMachine stateMachine;

    private float maxBounciness;
    private float bouncinessDecreaseAmount;

    private PhysicsMaterial2D bouncePhysics;
    private PhysicsMaterial2D noBouncePhysics;

    public Rigidbody2D Rb { get => rb; }

    public void SetUpBall(Vector2 spawnPos, float gravityScale, Player player, Vector2 playerOffset, float bounciness)
    {
        transform.position = spawnPos;
        
        rb.gravityScale = gravityScale;

        this.player = player;
        this.playerOffset = playerOffset;

        bouncePhysics = NewPhysicsMaterial("Bounce", bounciness, 20);

        //maxBounciness = ballMaxBounciness;

        gameObject.SetActive(true);
    }

    public void HitBall(Vector2 direction, float swingForce)
    {
        rb.velocity = direction * swingForce;
        stateMachine.ChangeState(BallStateMachine.State.Move);
    }

    private void Start()
    {
        stateMachine = GetComponent<BallStateMachine>();

        bouncePhysics = GetComponent<Collider2D>().sharedMaterial;
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stateMachine.CurrentState.OnCollisionEnter2D(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        stateMachine.CurrentState.OnCollisionExit2D(collision);
    }

    private void Update()
    {
        stateMachine.CurrentState.Update();
    }

    public void TeleportPlayerToBall()
    {
        stayPosition = transform.position;
        if (player != null)
        {
            player.ChangeState(PlayerStateMachine.State.Idle);
            player.transform.position = stayPosition + playerOffset;
        }
    }

    public void DecreaseBounciness()
    {
        if (bouncePhysics.bounciness <= 0.01f)
        {
            bouncePhysics.bounciness = 0;
            return;
        }

        if (bouncePhysics.bounciness > 0)
        {
            bouncePhysics.bounciness *= bouncinessDecreaseAmount;
        }

        Debug.Log("Decrease: " + bouncePhysics.bounciness + " max: " + maxBounciness);
    }

    public void ResetBounciness()
    {
        bouncePhysics = new PhysicsMaterial2D
        {
            bounciness = maxBounciness,
            friction = 20
        };

        coll.sharedMaterial = bouncePhysics;
    }

    private PhysicsMaterial2D NewPhysicsMaterial(string name, float bounciness, float friction)
    {
        return new PhysicsMaterial2D(name)
        {
            bounciness = bounciness,
            friction = friction
        };
    }
}
