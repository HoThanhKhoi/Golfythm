using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Swing : State<Player>
{
    private float fakeSpinAngle;
    private float peakAngle;

    private float increaseSpeedMultiplier = 10;
    private float decreaseSpeedMultiplier = 20;

    private float additionToPeak = 100;

    private int direction = 1;

    public PlayerState_Swing(string animBoolName, Player owner, StateMachine<Player> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fakeSpinAngle = owner.ClubSpinAngle;
        peakAngle = fakeSpinAngle + additionToPeak;

        Debug.Log(fakeSpinAngle + " " + additionToPeak);
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
            stateMachine.ChangeState(PlayerStateMachine.State.Hit);
        }
    }

    private float GetCurrentSpeedMultiplier()
    {
        return direction > 0 ? increaseSpeedMultiplier : decreaseSpeedMultiplier;
    }
}
