using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStoneGolem : BossStateOwner
{
    [Header("Fly to destination")]
    [SerializeField] private float flySpeed;
    [SerializeField] private Transform centerTransform;

    public float FlySpeed { get { return flySpeed; } }
    public Transform CenterTransform { get { return centerTransform; } }

    [Header("Attack")]
    [SerializeField] private float attackRange;

}
