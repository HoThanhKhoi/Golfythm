using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState_Stay : State<Ball>
{
    public BallState_Stay(string animBoolName, Ball owner, StateMachine<Ball> stateMachine) : base(animBoolName, owner, stateMachine)
    {
    }
}
