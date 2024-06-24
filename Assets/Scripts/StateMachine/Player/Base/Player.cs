using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateOwner
{
    [Header("Preference")]
    public InputReader inputReader;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotParent;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;


    [Header("Club Spin")]
    [SerializeField] private float maxClubSpinAngle;
    [SerializeField] private float minClubSpinAngle;
    public float ClubSpinAngle
    {
        get
        {
            float spinRange = maxClubSpinAngle - minClubSpinAngle;
            float forceRange = maxSwingForce - minSwingForce;

            float spinAngle = ((swingForce - minSwingForce) * spinRange) / forceRange + minClubSpinAngle;
            return spinAngle;
        }
    }

    public float ClubSpinSpeed
    {
        get
        {
            float spinRange = maxClubSpinAngle - minClubSpinAngle;
            float forceRange = maxSwingForce - minSwingForce;

            float spinSpeed = (SwingForceSpeed * spinRange) / forceRange;
            return spinSpeed;
        }
    }

    [Header("Swing Force")]
    [SerializeField] private float maxSwingForce;
    [SerializeField] private float minSwingForce;
    [SerializeField] private float swingForceSpeed;
    public float SwingForceSpeed { get { return swingForceSpeed; } }
    public float MaxSwingForce { get { return maxSwingForce; } }
    public float MinSwingForce { get { return minSwingForce; } }
    public float SwingForce
    {
        get { return swingForce; }
        set
        {
            swingForce = value;
            swingForce = Mathf.Clamp(swingForce, minSwingForce, maxSwingForce);
        }
    }
    private float swingForce;

    [Header("Dots")]
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private int numberOfDots;

    [Header("Ball")]
    [SerializeField] private float ballBounciness = .5f;
    [SerializeField] private float ballFriction = 1;
    [SerializeField] private float ballAirDrag = 0f;
    [SerializeField] private float ballGroundDrag = .8f;
    [SerializeField] private float stopMovingThreshold = .2f;
    [SerializeField] private float ballGroundCheckDistance = .3f;
    [SerializeField] private float ballGravity = 4;
    [SerializeField] private LayerMask groundLayer;

    public float BallAirDrag { get { return ballAirDrag; } }
    public float BallGroundDrag { get { return ballGroundDrag; } }

    public Vector2 HitDirection { get; set; }

    private GameObject[] dots;
    private Ball ball;

    public Rigidbody2D Rb { get; private set; }
    private CircleCollider2D coll;

    //Physic Material
    public PhysicsMaterial2D BounceMaterial { get; private set; }
    public PhysicsMaterial2D NoBounceMaterial { get; private set; }

    public GameObject PlayerVisual { get; private set; }
    public int PlayerVisualFacing { get => PlayerVisual.transform.right.x > 0 ? 1 : -1; }
    [SerializeField] private Vector2 playerVisualTransformOffset = new Vector2(0, 1f);
    private Transform club;

    private void Awake()
    {
        swingForce = minSwingForce;
        Rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        PlayerVisual = ObjectPoolingManager.Instance.SpawnFromPool("Player Visual", (Vector2)transform.position + playerVisualTransformOffset, Quaternion.identity);

        club = PlayerVisual.transform.Find("Club");

        Rb.gravityScale = ballGravity;

        PlayerVisual.SetActive(false);

        BounceMaterial = new PhysicsMaterial2D("Bounce")
        {
            bounciness = ballBounciness,
            friction = ballFriction
        };
        NoBounceMaterial = new PhysicsMaterial2D("No Bounce")
        {
            bounciness = 0,
            friction = ballFriction
        };

        GenerateDots();
        SetUpDotsPosition();

        SetPhysicMaterial(BounceMaterial);
        DotsActive(false);
    }

    public void SpinClub(float angle)
    {
        int facing = -PlayerVisualFacing;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle) * -facing);
        club.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, ClubSpinSpeed);
    }

    public void SlightlyChangeSwingForceTo(float to)
    {
        swingForce = Mathf.Lerp(swingForce, to, SwingForceSpeed * Time.deltaTime);
    }

    public void SetActivePlayerVisual(bool active)
    {
        if (active)
        {
            PlayerVisual = ObjectPoolingManager.Instance.SpawnFromPool("Player Visual", (Vector2)transform.position + playerVisualTransformOffset, Quaternion.identity);
        }
        else
        {
            PlayerVisual.SetActive(false);
        }
    }

    #region Aiming
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = ObjectPoolingManager.Instance.SpawnFromPool("Dot Trajectory", transform.position, Quaternion.identity);
            dots[i].transform.SetParent(dotParent.transform);
        }
    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }

    private Vector2 DotPosition(float t)
    {
        Vector2 position = (Vector2)dotParent.transform.position
            + inputReader.AimDirection * swingForce * t
            + 0.5f * Physics2D.gravity * ballGravity * t * t;

        return position;
    }

    public void SetUpDotsPosition()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].transform.position = DotPosition(i * spaceBetweenDots);
        }
    }

    #endregion

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, ballGroundCheckDistance + coll.radius, groundLayer);
    }

    public bool IsStopMoving()
    {
        return Rb.velocity.magnitude < stopMovingThreshold;
    }

    public void SetPhysicMaterial(PhysicsMaterial2D material)
    {
        coll.sharedMaterial = material;
    }

    public void HitBall(Vector2 direction, float swingForce)
    {
        Rb.velocity = direction * swingForce;
    }

    private void DrawCheckGround()
    {
        Debug.DrawRay(transform.position, Vector2.down * .5f, Color.red);
    }

    private void OnDrawGizmos()
    {
        DrawCheckGround();
    }
}
