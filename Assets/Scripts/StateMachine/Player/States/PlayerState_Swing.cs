using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Swing : State<Player>
{
    private float fakeSwingForce;
    private float peakSwing;

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

        fakeSwingForce = owner.SwingForce;
        peakSwing = fakeSwingForce + additionToPeak;
    }

    public override void Update()
    {
        base.Update();

        Swing();

        owner.SpinClub(fakeSwingForce);
    }

    private void Swing()
    {
        fakeSwingForce += Time.deltaTime * owner.PowerBarSpeed * GetCurrentSpeedMultiplier() * direction;

        Debug.Log("Speed: " + owner.PowerBarSpeed + " , current multiplier: " + GetCurrentSpeedMultiplier() + " , direction: " + direction);

        //fakeSwingForce += 1;

        if (fakeSwingForce >= peakSwing)
        {
            direction = -1;
        }

        if (fakeSwingForce < -100)
        {
            stateMachine.ChangeState(PlayerStateMachine.State.Hit);
        }

        Debug.Log("Fake " +  fakeSwingForce);
    }

    private float GetCurrentSpeedMultiplier()
    {
        return direction > 0 ? increaseSpeedMultiplier : decreaseSpeedMultiplier;
    }
}
