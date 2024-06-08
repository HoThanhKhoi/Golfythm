using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemArmProjectile : MonoBehaviour
{
    private Player player;
    private float speed;
    private float rotateSpeed;

    private float delayTime = 0.5f;
    private float delayTimer;

    private float scale;

    [SerializeField] private Rigidbody2D rb;

    public void SetUp(Player player, float speed, float rotateSpeed, Vector2 rightTransform)
    {
        this.player = player;
        this.speed = speed;
        this.rotateSpeed = rotateSpeed;

        delayTimer = delayTime;
        scale = 1f;

        rb.AddForce(rightTransform * 50, ForceMode2D.Impulse);
    }

    private void Update()
    {
        speed -= Time.deltaTime * 5;
        delayTimer -= Time.deltaTime;

        if (speed <= 10f)
        {
            scale -= Time.deltaTime * .05f;
            transform.localScale *= scale;

            if (scale <= .1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (delayTimer <= 0)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float rotateAmount = Vector3.Cross(transform.right, direction).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = -transform.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
