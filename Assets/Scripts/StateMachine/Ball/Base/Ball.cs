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

    private PhysicsMaterial2D bounce;
    private PhysicsMaterial2D noBounce;

    public Rigidbody2D Rb { get => rb; }

    public void SetUpBall(Vector2 spawnPos, Vector2 direction, float swingForce, float gravityScale, Player player, Vector2 playerOffset, float ballMaxBounciness, float ballDecreaseBouncinessAmount)
    {
        transform.position = spawnPos;
        rb.velocity = direction * swingForce;
        rb.gravityScale = gravityScale;

        this.player = player;
        this.playerOffset = playerOffset;

        maxBounciness = ballMaxBounciness;
        bouncinessDecreaseAmount = ballDecreaseBouncinessAmount;

        gameObject.SetActive(true);

        ResetBounciness();

        stateMachine.ChangeState(BallStateMachine.State.Move);
    }

    protected override void Awake()
    {
        stateMachine = new BallStateMachine(this);
    }

    private void Start()
    {
        bounce = GetComponent<Collider2D>().sharedMaterial;
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stateMachine.currentState.OnCollisionEnter2D(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        stateMachine.currentState.OnCollisionExit2D(collision);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
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
        if (bounce.bounciness <= 0.01f)
        {
            bounce.bounciness = 0;
            return;
        }

        if (bounce.bounciness > 0)
        {
            bounce.bounciness *= bouncinessDecreaseAmount;
        }

        Debug.Log("Decrease: " + bounce.bounciness + " max: " + maxBounciness);
    }

    public void ResetBounciness()
    {
        bounce = new PhysicsMaterial2D
        {
            bounciness = maxBounciness,
            friction = 20
        };

        coll.sharedMaterial = bounce;
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
