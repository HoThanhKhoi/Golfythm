using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateOwner : StateOwner
{
    [Header("Preference")]
    [SerializeField] protected Player player;
    [SerializeField] protected Rigidbody2D rb;

    public Rigidbody2D Rb { get { return rb; } }

    [Header("Detecting Player")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float detectionDistance = 0f;

    private LayerMask playerLayer;

    private void OnEnable()
    {
        playerLayer = player.gameObject.layer;
    }

    public Vector2 GetPlayerPosition()
    {
        Vector2 playerPosition = Vector2.zero;

        if (player != null)
        {
            playerPosition = player.transform.position;
        }

        return playerPosition;
    }

    public Vector2 GetDirectionToPlayer() => GetPlayerPosition() - (Vector2)transform.position;

    public void FaceToPlayer()
    {
        float xDirection = GetDirectionToPlayer().x > 0 ? 1 : -1;
        transform.right = new Vector2(xDirection, transform.right.y);
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public bool DetectPlayer()
    {
        Vector2 rayDirection = GetDirectionToPlayer();

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRadius, rayDirection, detectionDistance, playerLayer);

        if (hit.collider != null)
        {
            Player detectedPlayer;
            if (hit.transform.TryGetComponent<Player>(out detectedPlayer))
            {
                player = detectedPlayer;
                return true;
            }
        }

        DebugDrawCircleCast(transform.position, rayDirection, detectionRadius, detectionDistance, Color.red);

        return false;
    }

    void DebugDrawCircleCast(Vector2 origin, Vector2 direction, float radius, float distance, Color color)
    {
        Vector2 endPosition = origin + direction * distance;
        Debug.DrawLine(origin, endPosition, color);
        Debug.DrawLine(origin + new Vector2(radius, 0), endPosition + new Vector2(radius, 0), color);
        Debug.DrawLine(origin - new Vector2(radius, 0), endPosition - new Vector2(radius, 0), color);
    }
    public void MoveToPlayer(float speed)
    {
        rb.velocity = GetDirectionToPlayer() * speed;
    }

    public float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, GetPlayerPosition());
    }

    public void MoveToPosition(Vector2 position, float speed)
    {
        Vector2 moveDirection = position - (Vector2) transform.position;

        rb.velocity = moveDirection * speed;
    }

    public float DistanceToPosition(Vector2 position)
    {
        return Vector2.Distance((Vector2) transform.position, position);
    }
}
