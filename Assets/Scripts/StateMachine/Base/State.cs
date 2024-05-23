using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<TOwner> where TOwner : StateOwner
{
    protected string animBoolName;
    protected TOwner owner;
    protected StateMachine<TOwner> stateMachine;

    protected Animator anim;
    protected Rigidbody2D rb;

    public State(string animBoolName, TOwner owner, StateMachine<TOwner> stateMachine)
    {
        this.animBoolName = animBoolName;
        this.owner = owner;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        anim = owner.anim;
        rb = owner.rb;

        anim.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
