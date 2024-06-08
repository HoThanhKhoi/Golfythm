using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateOwnerAnimationTrigger<TOwner, EState> : MonoBehaviour where TOwner : StateOwner where EState : Enum
{
    [SerializeField] private StateMachine<TOwner, EState> stateMachine;

    protected virtual void TriggerAnimation(int index)
    {
        stateMachine.AnimationTrigger(index);
    }
}
