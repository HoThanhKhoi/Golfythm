using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateOwner
{
    [Header("Preference")]
    public InputReader inputReader;

    [Header("Settings")]
    [SerializeField] private Transform club;
    [SerializeField] private float maxSwingForce;
    [SerializeField] private float minSwingForce;
    [SerializeField] private float powerBarSpeed;

    public float MaxSwingForce { get { return maxSwingForce; } }

    public float MinSwingForce { get { return minSwingForce; } }

    [SerializeField] private float swingForce;

    public float SwingForce
    {
        get { return swingForce; }
        set 
        { 
            swingForce = value;
            swingForce = Mathf.Clamp(swingForce, minSwingForce, maxSwingForce);
        }
    }
    public float PowerBarSpeed { get { return powerBarSpeed; } }

    private PlayerStateMachine stateMachine;
    private int facing = 1;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        swingForce = minSwingForce;
        swingForce = Mathf.Clamp(swingForce, minSwingForce, maxSwingForce);
    }

    public void SpinClub(float angle)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle) * -facing);
        club.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, powerBarSpeed);
    }

    public void SlightlyChangeSwingForceTo(float to)
    {
        swingForce = Mathf.Lerp(swingForce, to, 10 * Time.deltaTime);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }
}
