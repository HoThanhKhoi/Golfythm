using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class StateData<EState> where EState : Enum
{
    public EState State;
    public AnimationClip AnimClip;
}
