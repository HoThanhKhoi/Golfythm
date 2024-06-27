using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Destroyed : State<Player, PlayerStateMachine.State>
{
    GameObject deathParticle;
    public PlayerState_Destroyed(Player owner, StateMachine<Player, PlayerStateMachine.State> stateMachine, Animator anim) : base(owner, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        owner.Rb.velocity = Vector2.zero;

        deathParticle = ObjectPoolingManager.Instance.SpawnFromPool("Player Death Paricle", owner.transform.position, Quaternion.identity);
        owner.SetActiveBallVisual(false);
        owner.Damage(1);
    }

    public override void Update()
    {
        base.Update();
        if(TimeOut())
        {
            RespawnPlayer();
            stateMachine.ChangeState(PlayerStateMachine.State.Ball);
        }
    }

    public void RespawnPlayer()
    {
        owner.MoveToCheckPoint();
        owner.CanSlowTime = false;
        owner.PlayerDirectionTowardsGrass = owner.SlowZone.transform.position - owner.Grass.transform.position;
        owner.SetActivePlayerVisual(true, owner.PlayerDirectionTowardsGrass);
        owner.SetActiveBallVisual(true);

        deathParticle.SetActive(false);
    }
}
