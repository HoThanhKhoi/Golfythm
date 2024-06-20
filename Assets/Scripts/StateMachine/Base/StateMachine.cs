using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TOwner, EState> : MonoBehaviour where TOwner : StateOwner where EState : Enum
{
    [SerializeField] private bool useAnimator = false;
    [SerializeField] protected Animator anim;
    protected TOwner owner;
    private State<TOwner, EState> currentState;

    public EState PrevEState { get; set; }
    public EState CurrentEState { get; set; }
    public State<TOwner, EState> CurrentState { get { return currentState; } }

    private Dictionary<EState, State<TOwner, EState>> stateDictionary;
    [field: SerializeField] public List<StateData<EState>> StateDataList { get; private set; }
    private void Awake()
    {
        owner = GetComponent<TOwner>();

        if (owner == null)
        {
            owner = gameObject.AddComponent<TOwner>();
        }

        stateDictionary = new Dictionary<EState, State<TOwner, EState>>();

        SetUpStateMachine();
    }

    protected abstract void SetUpStateMachine();

    public void ChangeState(EState eState)
    {
        if (currentState == stateDictionary[eState])
        {
            return;
        }

        if (currentState != null)
        {
            PrevEState = CurrentEState;
            currentState.Exit();
        }

        currentState = stateDictionary[eState];
        CurrentEState = eState;
        currentState.Enter();
    }

    public void ChangeState(EState eState, float delay)
    {
        StartCoroutine(ChangeStateAfterDelayCo(eState, delay));
    }

    private IEnumerator ChangeStateAfterDelayCo(EState eState, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(eState);
    }

    protected void AddState(EState eState, State<TOwner, EState> state)
    {
        string eStateName = eState.ToString();

        if (useAnimator)
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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionEnter2D(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionExit2D(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerEnter2D(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerExit2D(collision);
        }
    }

    public virtual void AnimationTrigger(int index)
    {
        currentState.AnimationTrigger(index);
    }
}
