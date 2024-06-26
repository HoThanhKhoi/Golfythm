using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TimeManager TimeManager {  get; private set; }

    private void Start()
    {
        TimeManager = new TimeManager();
    }
}
