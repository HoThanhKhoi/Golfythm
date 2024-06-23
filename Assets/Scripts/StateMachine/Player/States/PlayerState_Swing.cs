using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Swing : State<Player, PlayerStateMachine.State>
{
    private float fakeSpinAngle;
    private float peakAngle;

    private float increaseSpeedMultiplier = 10;
    private float decreaseSpeedMultiplier = 20;

    private float additionToPeak = 100;

    private int direction = 1;

    public PlayerState_Swing(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fakeSpinAngle = owner.ClubSpinAngle;
        peakAngle = fakeSpinAngle + additionToPeak;
    }

    public override void Update()
    {
        base.Update();

        Swing();

        owner.SpinClub(fakeSpinAngle);
    }

    private void Swing()
    {
        fakeSpinAngle += Time.deltaTime * owner.ClubSpinAngle * GetCurrentSpeedMultiplier() * direction;

        if (fakeSpinAngle >= peakAngle)
        {
            direction = -1;
        }

        if (fakeSpinAngle < -100)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Ball);
        }
    }

    private float GetCurrentSpeedMultiplier()
    {
        return direction > 0 ? increaseSpeedMultiplier : decreaseSpeedMultiplier;
    }
}
