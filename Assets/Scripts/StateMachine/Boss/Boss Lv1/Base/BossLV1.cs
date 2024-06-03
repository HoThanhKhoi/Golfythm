using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1 : StateOwner
{
    private BossLV1_StateMachine stateMachine;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
       
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
