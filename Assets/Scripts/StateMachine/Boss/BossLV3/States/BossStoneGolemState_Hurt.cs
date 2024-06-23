using System.Collections;
using UnityEngine;

public class BossStoneGolemState_Hurt : State<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    private float initMass;
    private readonly float tempMass = 1;
    public BossStoneGolemState_Hurt(BossStoneGolem owner, StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = animationLength * 5;

        initMass = owner.Rb.mass;
        owner.Rb.mass = tempMass;
    }

    public override void Update()
    {
        base.Update();

        if(TimeOut())
        {
            stateMachine.ChangeState(BossStoneGolemStateMachine.State.FlyToCenter);
        }
    }

    public override void Exit()
    {
        base.Exit();

        owner.Rb.mass = initMass;
    }
}