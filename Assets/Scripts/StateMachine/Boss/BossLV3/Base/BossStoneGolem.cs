using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStoneGolem : BossStateOwner
{
    [Header("Fly to destination")]
    [SerializeField] private float flySpeed;
    [SerializeField] private Transform centerTransform;

    public float FlySpeed { get { return flySpeed; } }
    public Transform CenterTransform { get { return centerTransform; } }

    [Header("Attack")]
    [SerializeField] private float attackRange;

    [Header("Projectile")]
    [SerializeField] private GameObject armProjectilePrefab;
    [SerializeField] private Transform armProjectileSpawnPoint;
    [SerializeField] private float armProjectileSpeed = 50f;
    [SerializeField] private float armProjectileRotateSpeed = 100f;
    [SerializeField] private int maxProjectileCount = 3;
    [SerializeField] private float projectileCooldown = 1f;

    public float ProjectileCooldown { get { return projectileCooldown; } }

    [Header("Zip")]
    [SerializeField] private LineRenderer zipIndicator;
    [SerializeField] private float zipShootSpeed = 30f;
    [SerializeField] private float zipShootCooldown = 1f;
    [SerializeField] private float distanceToPlayerToShoot = 10f;
    [SerializeField] private float maxZipShootCount = 3;
    public float ZipShootCooldown { get { return zipShootCooldown; } }

    [Header("Lazer Cast")]
    [SerializeField] private int maxLazerCastCount = 3;
    [SerializeField] private GameObject lazerCastPrefab;
    [SerializeField] private Transform lazerCastSpawnPoint;
    [SerializeField] private float lazerCastSpeed = 30f;
    [SerializeField] private float lazerCastCooldown = 1f;


    private int armProjectileCount = 0;
    private int zipShootCount = 0;

    private void Start()
    {
        armProjectileCount = 0;
    }

    public void SpawnArmProjectile()
    {
        GameObject armProjectile = Instantiate(armProjectilePrefab, armProjectileSpawnPoint.transform.position, Quaternion.identity);

        GolemArmProjectile projectile = armProjectile.GetComponent<GolemArmProjectile>();
        projectile.SetUp(player, armProjectileSpeed, armProjectileRotateSpeed, (Vector2)transform.right);

        armProjectileCount++;
    }

    public bool IsProjectileCountFull() => armProjectileCount >= maxProjectileCount;

    public bool IsZipShootCountFull() => zipShootCount >= maxZipShootCount;

    public void SetProjectileCountToZero() => armProjectileCount = 0;

    public void SetZipShootCountToZero() => zipShootCount = 0;

    public void ShootSelfToPlayer()
    {
        zipShootCount++;
        rb.AddForce(GetDirectionToPlayer() * zipShootSpeed, ForceMode2D.Impulse);
    }

    public void SetActiveZipIndicator(bool active)
    {
        if(active)
        {
            if(zipIndicator != null)
            {
                zipIndicator.gameObject.SetActive(true);

                zipIndicator.SetPosition(0, transform.position);
                zipIndicator.SetPosition(1, player.transform.position);
            }
        }

        else
        {
            if(zipIndicator != null)
            {
                zipIndicator.gameObject.SetActive(false);
            }
        }
    }
}
