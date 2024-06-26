using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemArmProjectile : MonoBehaviour
{
    private Player player;
    private float speed;
    private float rotateSpeed;
    private bool isFlipped;

    private float initialDelay = 0.5f;
    private float delayTimer;

    [SerializeField] private Rigidbody2D rb;

    public void SetUp(Player player, float speed, float rotateSpeed, Vector2 rightTransform, bool isFlipped, float initialDelay)
    {
        this.player = player;
        this.speed = speed;
        this.rotateSpeed = rotateSpeed;
        this.isFlipped = isFlipped;
        this.initialDelay = initialDelay;

        delayTimer = initialDelay;

        rb.AddForce(rightTransform * 50, ForceMode2D.Impulse);
    }

    private void Update()
    {
        speed -= Time.deltaTime * 5;
        delayTimer -= Time.deltaTime;

        if (speed <= 10f)
        {
            ObjectPoolingManager.Instance.SpawnFromPool("Laser Impact", transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (delayTimer <= 0)
        {
            FollowPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectPoolingManager.Instance.SpawnFromPool("Laser Impact", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void FollowPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        Vector2 adjustedDirection = isFlipped ? -direction : direction;
        Vector2 adjustedRightTransform = isFlipped? -transform.right : transform.right;

        float rotateAmount = Vector3.Cross(adjustedDirection, -transform.right).z;
        rb.angularVelocity = rotateAmount * rotateSpeed;

        rb.velocity = adjustedRightTransform * speed;
    }
}
