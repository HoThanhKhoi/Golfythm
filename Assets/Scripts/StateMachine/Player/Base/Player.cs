using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateOwner
{
    [SerializeField] private Transform club;

    public void SpinClub()
    {
        Debug.Log("Spin");
    }
}
