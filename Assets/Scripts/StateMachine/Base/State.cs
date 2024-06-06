using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<TOwner, EState> where TOwner : StateOwner where EState : Enum
{
    protected TOwner owner;
    protected StateMachine<TOwner, EState> stateMachine;

    protected Animator anim;
    protected float stateTimer;

    private AnimationClip animClip;
    private int animationHash;
    protected float animationLength;
    private float animationTimer;

    #region Setup
    public State(TOwner owner, StateMachine<TOwner, EState> stateMachine, Animator anim)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        this.anim = anim;
    }

    public void SetupAnimationClip(AnimationClip clip)
    {
        animClip = clip;
        animationHash = Animator.StringToHash(clip.name);
        animationLength = clip.length;
    }
    #endregion

    #region State Function
    public virtual void Enter()
    {
        animationTimer = animationLength;

        PlayAnimationFromBeginning();
    }
    public virtual void Exit()
    {

    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        animationTimer -= Time.deltaTime;
    }
    public virtual void FixedUpdate() { }
    public virtual void OnTriggerEnter2D(Collider2D other) { }
    public virtual void OnTriggerExit2D(Collider2D other) { }
    public virtual void OnCollisionEnter2D(Collision2D other) { }
    public virtual void OnCollisionExit2D(Collision2D other) { }
    #endregion

    #region Utils
    public virtual bool TimeOut()
    {
        return stateTimer <= 0;
    }
    #endregion

    #region Animation Functions
    protected virtual void PlayAnimationFromBeginning()
    {
        if (anim != null)
        {
            anim.Play(animationHash, 0, 0.0f);
        }
    }

    protected virtual bool IsAnimationFinished()
    {
        return animationTimer <= 0;
    }

    protected virtual void StopAnimation()
    {
        anim.speed = 0;
    }
    #endregion
}
