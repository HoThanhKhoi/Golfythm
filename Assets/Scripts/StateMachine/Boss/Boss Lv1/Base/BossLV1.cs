using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1 : MonoBehaviour
{
    private BossLV1_StateMachine stateMachine;

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
