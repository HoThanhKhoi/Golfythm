using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateOwner
{
    [SerializeField] private Transform club;
    public InputReader inputReader;

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
    }

    private void OnEnable()
    {
        inputReader.TouchEvent += OnTouch;
    }

    private void OnDisable()
    {
        inputReader.TouchEvent -= OnTouch;
    }

    private void OnTouch(bool touch)
    {
        Debug.Log("Touch");
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
