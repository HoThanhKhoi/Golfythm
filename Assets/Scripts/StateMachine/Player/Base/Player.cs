using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateOwner
{
    [SerializeField] private Transform club;
    [SerializeField] private InputReader inputReader;

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
    }

    public void SpinClub()
    {
        Debug.Log("Spin");
    }

    private void Update()
    {
        Debug.Log(inputReader.AimDirection);
    }
}
