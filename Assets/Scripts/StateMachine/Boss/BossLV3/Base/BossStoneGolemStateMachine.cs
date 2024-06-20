using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStoneGolemStateMachine : StateMachine<BossStoneGolem, BossStoneGolemStateMachine.State>
{
    public enum State
    {
        Born,
        FlyToCenter,
        Idle,
        ArmorBuff,
        Glowing,
        Imune,
        LaserCast,
        MeleeAttack,
        RangeAttack,
        Hurt,
        Death
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Born, new BossStoneGolemState_Born(owner, this, anim));
        AddState(State.FlyToCenter, new BossStoneGolemState_FlyToCenter(owner, this, anim));
        AddState(State.Idle, new BossStoneGolemState_Idle(owner, this, anim));
        AddState(State.ArmorBuff, new BossStoneGolemState_ArmorBuff(owner, this, anim));
        AddState(State.Glowing, new BossStoneGolemState_Glowing(owner, this, anim));
        AddState(State.Imune, new BossStoneGolemState_Imune(owner, this, anim));
        AddState(State.LaserCast, new BossStoneGolemState_LaserCast(owner, this, anim));
        AddState(State.MeleeAttack, new BossStoneGolemState_MeleeAttack(owner, this, anim));
        AddState(State.RangeAttack, new BossStoneGolemState_RangeAttack(owner, this, anim));
        AddState(State.Hurt, new BossStoneGolemState_Hurt(owner, this, anim));
        AddState(State.Death, new BossStoneGolemState_Death(owner, this, anim));

        ChangeState(State.Born);
    }
}
