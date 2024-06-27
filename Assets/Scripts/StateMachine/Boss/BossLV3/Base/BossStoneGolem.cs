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

    [Header("Idle")]
    [SerializeField] private float idleTime;
    public float IdleTime { get { return idleTime; } }

    [Header("Rest")]
    [SerializeField] private float restTime;
    [SerializeField] private float restFallSpeed;

    public float RestTime { get { return restTime; } }
    public float RestFallSpeed { get {return restFallSpeed; } }

    [Header("Projectile")]
    [SerializeField] private GameObject armProjectilePrefab;
    [SerializeField] private Transform armProjectileSpawnPoint;
    [SerializeField] private float armProjectileSpeed = 50f;
    [SerializeField] private float armProjectileRotateSpeed = 100f;
    [SerializeField] private float projectileCooldown = 1f;
    [SerializeField] private float projectileInitialDelay = 0.5f;
    [SerializeField] private int maxProjectileCount = 3;

    public float ProjectileCooldown { get { return projectileCooldown; } }

    [Header("Zip")]
    [SerializeField] private LineRenderer zipIndicator;
    [SerializeField] private float zipShootSpeed = 30f;
    [SerializeField] private float zipShootCooldown = 1f;
    [SerializeField] private float maxZipShootCount = 3;
    public float ZipShootCooldown { get { return zipShootCooldown; } }

    [Header("Lazer Cast")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject laserOrigin;
    [SerializeField] private GameObject laserBeam;
    [SerializeField] private GameObject laserParent;
    [SerializeField] private GameObject laserImpact;
    [SerializeField] private LayerMask laserCollisionLayer;
    [SerializeField] private LayerMask laserImpactLayer;

    [SerializeField] private int maxLazerCastCount = 3;
    [SerializeField] private float maxLazerLength = 500f;
    [SerializeField] private float laserShootTime = 3f;

    [SerializeField] private float laserRotationSpeed = 2f;

    public float LaserShootTime { get { return laserShootTime; } }

    private SpriteRenderer laserBeamSpriteRenderer;
    private Vector2 currentHitPoint;

    private int armProjectileCount = 0;
    private int zipShootCount = 0;
    private int laserCastCount = 0;

    protected override void Start()
    {
        base.Start();
        armProjectileCount = 0;

        laserOrigin.SetActive(false);
        laserBeam.SetActive(false);

        laserBeamSpriteRenderer = laserBeam.GetComponent<SpriteRenderer>();
    }

    public void SpawnArmProjectile()
    {
        bool isFLipped = transform.right.x < 0;
        GameObject armProjectile = ObjectPoolingManager.Instance.SpawnFromPool("Stone Golem Arm", armProjectileSpawnPoint.position, Quaternion.identity);

        GolemArmProjectile projectile = armProjectile.GetComponent<GolemArmProjectile>();
        projectile.SetUp(player, armProjectileSpeed, armProjectileRotateSpeed, (Vector2)transform.right, isFLipped, projectileInitialDelay);

        armProjectileCount++;
    }

    public bool IsProjectileCountFull() => armProjectileCount >= maxProjectileCount;

    public bool IsZipShootCountFull() => zipShootCount >= maxZipShootCount;

    public bool IsLaserCastCountFull() => laserCastCount >= maxLazerCastCount;

    public void ShootSelfToPlayer()
    {
        zipShootCount++;

        rb.AddForce(GetDirectionToPlayer(transform.position) * (zipShootSpeed * Rb.mass), ForceMode2D.Impulse);
    }

    public void SetActiveZipIndicator(bool active)
    {
        if (active)
        {
            if (zipIndicator != null)
            {
                zipIndicator.gameObject.SetActive(true);

                zipIndicator.SetPosition(0, transform.position);
                zipIndicator.SetPosition(1, player.transform.position);
            }
        }

        else
        {
            if (zipIndicator != null)
            {
                zipIndicator.gameObject.SetActive(false);
            }
        }
    }

    public void CastLaser()
    {
        laserCastCount++;
        laserOrigin.SetActive(true);
    }

    public void LaserStartShoot()
    {
        Animator laserOriginAnimator = laserOrigin.GetComponent<Animator>();
        laserOriginAnimator.SetBool("Shooting", true);
        laserBeam.SetActive(true);
    }

    public void ShootingLaser()
    {
        PointLaserToPlayer();
        RayCastLaser();
    }

    public void EndShootingLaser()
    {
        laserBeam.SetActive(false);
        laserOrigin.SetActive(false);
    }

    public void PointLaserToPlayer()
    {
        Vector2 direction = GetDirectionToPlayer(laserOrigin.transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        laserParent.transform.rotation = Quaternion.Lerp(laserParent.transform.rotation, targetRotation, laserRotationSpeed * Time.deltaTime);
    }

    private void RayCastLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserOrigin.transform.position, laserParent.transform.right, Mathf.Infinity, laserCollisionLayer);
        RaycastHit2D impactHit = Physics2D.Raycast(laserOrigin.transform.position, laserParent.transform.right, Mathf.Infinity, laserImpactLayer);

        float laserLength = maxLazerLength;

        if (hit.collider != null)
        {
            if (Vector2.Distance(hit.point, currentHitPoint) >= .1f)
            {
                currentHitPoint = hit.point;
            }

            if (!impactHit.collider.CompareTag("FX"))
            {
                GameObject impactInstacne = ObjectPoolingManager.Instance.SpawnFromPool("Laser Impact", hit.point, Quaternion.identity);
            }

            laserLength = Vector2.Distance(laserOrigin.transform.position, hit.point);
        }

        ChangeLaserSize(laserLength);
    }

    private void ChangeLaserSize(float size)
    {
        laserBeamSpriteRenderer.size = new Vector2(size, laserBeamSpriteRenderer.size.y);
    }

    public void ResetAttackCount()
    {
        armProjectileCount = 0;
        zipShootCount = 0;
        laserCastCount = 0;
    }
}