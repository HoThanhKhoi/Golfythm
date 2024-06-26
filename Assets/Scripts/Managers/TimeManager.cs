using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    public float currentTimeScale;
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
