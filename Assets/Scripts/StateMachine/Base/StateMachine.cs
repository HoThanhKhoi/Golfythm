using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TOwner> where TOwner : StateOwner
{
    protected TOwner owner;
    public State<TOwner> currentState;
    private Dictionary<Enum, State<TOwner>> states;

    public StateMachine(TOwner owner)
    {
        this.owner = owner;

        states = new Dictionary<Enum, State<TOwner>>();
    }

    protected abstract void SetUpStateMachine();

    public void ChangeState(Enum eState)
    {
        if (currentState == states[eState])
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = states[eState];
        currentState.Enter();
    }

    protected void AddState(Enum eState, State<TOwner> state)
    {
        states.Add(eState, state);
    }

    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }
}
