using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StateOwner : MonoBehaviour
{
    public Animator anim { get; private set; }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
