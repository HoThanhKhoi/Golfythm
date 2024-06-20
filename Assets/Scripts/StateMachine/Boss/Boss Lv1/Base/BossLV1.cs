using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1 : StateOwner
{
    private BossLV1_StateMachine stateMachine;

    private void Start()
    {

    }

    private void Update()
    {
        stateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }
}
