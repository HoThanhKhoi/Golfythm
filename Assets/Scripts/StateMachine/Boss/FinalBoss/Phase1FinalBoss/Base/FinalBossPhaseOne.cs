using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhaseOne : BossStateOwner
{
	[Header("Boss Center")]
	[SerializeField] private Transform bossCenter;
	public Transform BossCenter => bossCenter;

	[Header("Idle")]
    [SerializeField] private float idleDuration = 7f;

	public float IdleDuration => idleDuration;
	//public float IdleDuration { get => idleDuration; }
	//public float IdleDuration { get { return idleDuration; } }
    //3 cau i chang nhau

	[Header("Run")]
    [SerializeField] private float runSpeed = 5f;
	public float RunSpeed => runSpeed;

	[SerializeField] private float runDuration = 10f;
	public float RunDuration => runDuration;


	[Header("Attack")]
	[SerializeField] private float attackRange = 1f;
	public float AttackRange => attackRange;
}