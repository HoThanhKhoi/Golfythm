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
        Zip,
        LaserCast,
        MeleeAttack,
        RangeAttack,
        Hurt,
        Death,
        LaserShoot
    }

    protected override void SetUpStateMachine()
    {
        AddState(State.Born, new BossStoneGolemState_Born(owner, this, anim));
        AddState(State.FlyToCenter, new BossStoneGolemState_FlyToCenter(owner, this, anim));
        AddState(State.Idle, new BossStoneGolemState_Idle(owner, this, anim));
        AddState(State.ArmorBuff, new BossStoneGolemState_ArmorBuff(owner, this, anim));
        AddState(State.Glowing, new BossStoneGolemState_Glowing(owner, this, anim));
        AddState(State.Zip, new BossStoneGolemState_Zip(owner, this, anim));
        AddState(State.LaserCast, new BossStoneGolemState_LaserCast(owner, this, anim));
        AddState(State.MeleeAttack, new BossStoneGolemState_MeleeAttack(owner, this, anim));
        AddState(State.RangeAttack, new BossStoneGolemState_RangeAttack(owner, this, anim));
        AddState(State.Hurt, new BossStoneGolemState_Hurt(owner, this, anim));
        AddState(State.Death, new BossStoneGolemState_Death(owner, this, anim));
        AddState(State.LaserShoot, new BossStoneGolemState_LaserShoot(owner, this, anim));

        ChangeState(State.Born);
    }
}
