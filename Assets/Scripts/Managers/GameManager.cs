using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TimeManager TimeManager {  get; private set; }
    [SerializeField] private Transform player;
    [SerializeField] private Transform bossStateOwner;
    [field:SerializeField] public CameraHandler cameraHandler { get; private set; }

    private void Start()
    {
        TimeManager = new TimeManager();

        AddPlayerToTarget();
    }

    public void AddBossToTarget()
    {
        cameraHandler.AddToTarget(bossStateOwner, 1, 10);
    }

    public void AddPlayerToTarget()
    {
        cameraHandler.AddToTarget(player, 1, 10);
    }

    public void RemovePlayerToTarget()
    {
        cameraHandler.RemoveFromTarget(player);
    }

    public void RemoveBossFromTarget()
    {
        cameraHandler.RemoveFromTarget(bossStateOwner);
    }

    public void StartBossState()
    {
        AddBossToTarget();
    }
}
