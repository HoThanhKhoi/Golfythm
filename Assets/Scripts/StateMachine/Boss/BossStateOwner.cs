using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class BossStateOwner : StateOwner
{
    [Header("Preference")]
    [SerializeField] protected Player player;
    [SerializeField] protected Rigidbody2D rb;

    public Rigidbody2D Rb { get { return rb; } }

    [Header("Detecting Player")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float detectionDistance = 0f;

    [SerializeField] private LayerMask detectionLayer;

    public Vector2 GetPlayerPosition()
    {
        Vector2 playerPosition = Vector2.zero;

        if (player != null)
        {
            playerPosition = player.transform.position;
        }

        return playerPosition;
    }

    public Vector2 GetDirectionToPlayer(Vector2 origin) => (GetPlayerPosition() - origin).normalized;

    public void FaceToPlayer()
    {
        FaceTo(GetPlayerPosition());
    }

    public void FaceToPlayer(Vector2 origin)
    {
        FaceTo(origin, GetPlayerPosition());
    }

    public void FaceTo(Vector2 destination)
    {
        float xDirection = destination.x > transform.position.x ? 1 : -1;
        transform.right = new Vector2(xDirection, transform.right.y);
    }

    public void FaceTo(Vector2 origin, Vector2 destination)
    {
        float xDirection = destination.x > origin.x ? 1 : -1;
        transform.right = new Vector2(xDirection, transform.right.y);
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public bool DetectPlayer(Vector2 origin)
    {
        Vector2 rayDirection = GetDirectionToPlayer(origin);

        RaycastHit2D hit = Physics2D.CircleCast(origin, detectionRadius, rayDirection, detectionDistance, detectionLayer);

        DebugDrawCircleCast(origin, detectionRadius, rayDirection, detectionDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void MoveToPlayer(float speed)
    {
        MoveToPosition(GetPlayerPosition(), speed);
    }

    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, GetPlayerPosition());
    }

    public float GetDistanceToPlayer(Vector2 origin)
    {
        return Vector2.Distance(origin, GetPlayerPosition());
    }

    public void MoveToPosition(Vector2 destination, float speed)
    {
        Vector2 moveDirection = destination - (Vector2)transform.position;

        rb.velocity = moveDirection * speed;
    }

    public void MoveToPosition(Vector2 origin, Vector2 destination, float speed)
    {
        Vector2 moveDirection = destination - origin;

        rb.velocity = moveDirection * speed;
    }

    public float GetDistanceToPosition(Vector2 position)
    {
        return Vector2.Distance((Vector2)transform.position, position);
    }

    public float GetDistanceToPosition(Vector2 origin, Vector2 position)
    {
        return Vector2.Distance(origin, position);
    }

    #region Debug Gizmos
    void DebugDrawCircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
    {
        // Draw the initial circle
        DebugDrawCircle(origin, radius, Color.red);

        // Draw the final circle
        Vector2 endPosition = origin + direction.normalized * distance;
        DebugDrawCircle(endPosition, radius, Color.red);

        // Draw the connecting line
        Debug.DrawLine(origin, endPosition, Color.red);
    }

    void DebugDrawCircle(Vector2 position, float radius, Color color)
    {
        int segments = 20;
        float angle = 0f;
        float angleStep = 360f / segments;

        Vector3 prevPoint = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

        for (int i = 1; i <= segments; i++)
        {
            angle += angleStep;
            Vector3 newPoint = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            Debug.DrawLine(prevPoint, newPoint, color);
            prevPoint = newPoint;
        }
    }

    #endregion
}
