using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TOwner> : MonoBehaviour where TOwner: StateOwner
{
    protected TOwner owner;
    public State<TOwner> currentState;
    private Dictionary<Type, State<TOwner>> states = new Dictionary<System.Type, State<TOwner>>();

    protected virtual void Awake()
    {
        SetUpStateMachine();
    }

    protected abstract void SetUpStateMachine();

    public void ChangeState<T>() where T : State<TOwner>
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = states[typeof(T)];
        currentState.Enter();   
    }

    protected void AddState(State<TOwner> state)
    {
        states.Add(state.GetType(), state);
    }

    protected virtual void Update()
    {
        if(currentState !=null)
        {
            currentState.Update();
        }
    }

    protected virtual void FixedUpdate()
    {
        if(currentState !=null)
        {
            currentState.FixedUpdate();
        }
    }
}
