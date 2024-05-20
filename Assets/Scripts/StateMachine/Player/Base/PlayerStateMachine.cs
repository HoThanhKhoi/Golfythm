using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<Player>
{
    protected override void SetUpStateMachine()
    {
        AddState(new PlayerState_Idle("Idle", owner, this));
        AddState(new PlayerState_Move("Move", owner, this));

        ChangeState<PlayerState_Idle>();
    }
}
