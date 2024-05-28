using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : StateOwner
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    private Player player;
    private Vector2 playerOffset;

    private bool isColliding = false;
    private Vector2 stayPosition;

    private BallStateMachine stateMachine;

    public Rigidbody2D Rb { get => rb; }

    public void SetUpBall(Vector2 spawnPos, Vector2 direction, float swingForce, float gravityScale, Player player, Vector2 playerOffset)
    {
        transform.position = spawnPos;
        rb.velocity = direction * swingForce;
        rb.gravityScale = gravityScale;

        this.player = player;
        this.playerOffset = playerOffset;
        gameObject.SetActive(true);

        stateMachine.ChangeState(BallStateMachine.State.Move);
    }

    protected override void Awake()
    {
        stateMachine = new BallStateMachine(this);
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();

        //if (isColliding)
        //{
        //    rb.velocity = rb.velocity * 0.92f;
        //}

        //if (rb.velocity.magnitude < 0.1f)
        //{
        //    rb.velocity = Vector2.zero;
        //    stayPosition = transform.position;
        //    if (player != null)
        //    {
        //        player.ChangeState(PlayerStateMachine.State.Idle);
        //        player.transform.position = stayPosition + playerOffset;
        //    }
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
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

    public void SlowDownBallOvertime()
    {

    }
}
