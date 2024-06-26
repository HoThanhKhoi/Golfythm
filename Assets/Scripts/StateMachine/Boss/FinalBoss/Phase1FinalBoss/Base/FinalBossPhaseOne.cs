using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FinalBossPhaseOne : BossStateOwner
{
	[SerializeField] private float tolerance = 0.75f;
	public float Tolerance => tolerance;

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
    [SerializeField] private float runSpeed = 10f;
	public float RunSpeed => runSpeed;

	[SerializeField] private float runDuration = 5f;
	public float RunDuration => runDuration;

	private int runCounter = 3;
	public int RunCounter => runCounter;

	[Header("Dash")]
	[SerializeField] private float dashDuration = 2f;
	public float DashDuration => dashDuration;

	[SerializeField] private float dashSpeed = 50f;
	public float DashSpeed => dashSpeed;

	[Header("Jump")]
	[SerializeField] private float jumpDuration = 3f;
	public float JumpDuration => jumpDuration;

	[SerializeField] private float jumpSpeed = 10f;
	public float JumpSpeed => jumpSpeed;

	

	[Header("Shield Crash")]
	[SerializeField] private float shieldCrashSpeed = 50f;
	public float ShieldCrashSpeed => shieldCrashSpeed;

	[Header("Attack")]
	[SerializeField] private float attackRange = 1f;
	public float AttackRange => attackRange;




	public void DecreaseAmount(ref int returnValue,int amount)
	{
		returnValue -= amount;
	}

	public void DecreaseRunCounter()
	{
		DecreaseAmount(ref runCounter, 1);
	}

	public void ResetRunCounter() 
	{
		runCounter = 3;
	}
}