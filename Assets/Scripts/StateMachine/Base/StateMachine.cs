using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TOwner, EState> : MonoBehaviour where TOwner : MonoBehaviour where EState : Enum
{
    [SerializeField] private bool useAnimator = false;
    [SerializeField] protected Animator anim;
    protected TOwner owner;
    public State<TOwner, EState> currentState;

    private Dictionary<Enum, State<TOwner, EState>> stateDictionary;
    [field: SerializeField] public List<StateData<EState>> StateDataList { get; private set; }


    private void Awake()
    {
        owner = GetComponent<TOwner>();

        if (owner == null)
        {
            owner = gameObject.AddComponent<TOwner>();
        }

        stateDictionary = new Dictionary<Enum, State<TOwner, EState>>();

        SetUpStateMachine();
    }

    protected abstract void SetUpStateMachine();

    public void ChangeState(Enum eState)
    {
        if (currentState == stateDictionary[eState])
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = stateDictionary[eState];
        currentState.Enter();
    }

    protected void AddState(EState eState, State<TOwner, EState> state)
    {
        string eStateName = eState.ToString();

        if(useAnimator)
        {
            //Add animation clip to state
            StateData<EState> stateData = StateDataList.Find(x => x.State.ToString() == eStateName);
            if (stateData != null)
            {
                AnimationClip clip = stateData.AnimClip;

                if (clip != null)
                {
                    state.SetupAnimationClip(clip);
                }
            }
        }

        stateDictionary.Add(eState, state);
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
