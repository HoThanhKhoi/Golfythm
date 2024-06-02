using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<TOwner, EState> where TOwner : StateOwner where EState : Enum
{
    public AnimationClip animClip;
    protected TOwner owner;
    protected StateMachine<TOwner, EState> stateMachine;

    protected Animator anim;
    protected float stateTimer;

    public State(TOwner owner, StateMachine<TOwner, EState> stateMachine)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        anim = owner.anim;

        if (anim != null)
        {
            anim.Play(animClip.name);
        }
    }
    public virtual void Exit()
    {

    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void FixedUpdate() { }

    public virtual bool TimeOut()
    {
        return stateTimer <= 0;
    }

    public virtual void OnTriggerEnter2D(Collider2D other) { }
    public virtual void OnTriggerExit2D(Collider2D other) { }
    public virtual void OnCollisionEnter2D(Collision2D other) { }
    public virtual void OnCollisionExit2D(Collision2D other) { }
}
