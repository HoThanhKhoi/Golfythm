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
    [SerializeField] private Transform club;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Club Spin")]
    [SerializeField] private float maxClubSpinAngle;
    [SerializeField] private float minClubSpinAngle;

    public AnimationClip something;
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
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPos;
    [SerializeField] private float ballGravity;
    [SerializeField] private float ballBounciness = .5f;
    [SerializeField] private float ballHaha;
    public Vector2 HitDirection { get; set; }

    //private PlayerStateMachine stateMachine;
    private int facing = 1;
    private GameObject[] dots;
    private Ball ball;

    protected override void Awake()
    {
        base.Awake();

        //stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        swingForce = minSwingForce;
        //ball = Instantiate(ballPrefab, ballSpawnPos.position, Quaternion.identity).GetComponent<Ball>();

        //ball.SetUpBall()

        GenerateDots();
        SetUpDotsPosition();
    }

    public void SpinClub(float angle)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle) * -facing);
        club.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, ClubSpinSpeed);
    }

    public void SlightlyChangeSwingForceTo(float to)
    {
        swingForce = Mathf.Lerp(swingForce, to, SwingForceSpeed * Time.deltaTime);
    }

    #region Aiming

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
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
        Vector2 position = (Vector2) dotParent.transform.position
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

    #region Hit ball
    public void SetUpBall()
    {
        Vector2 playerOffSet = transform.position - ballSpawnPos.position;
        //ball.SetUpBall(ballSpawnPos.position, HitDirection, swingForce, ballGravity, this, playerOffSet, ballBounciness);
    }
    #endregion

    #region Camera Handling
    private void CameraFollow(Transform target)
    {
        virtualCamera.m_Follow = target;
    }

    public void CameraFollowBall()
    {
        CameraFollow(ball.transform);
    }
    #endregion

    #region Handle State Transition
    public void ChangeState(Enum state)
    {
    }
    #endregion
}
