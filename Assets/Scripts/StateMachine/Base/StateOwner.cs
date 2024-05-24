using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOwner : MonoBehaviour
{
    public bool useAnimator = true;

    public Animator anim { get; private set; }

    protected virtual void Awake()
    {
        if (useAnimator)
        {
            anim = GetComponent<Animator>();

            if (anim == null)
            {
                anim = gameObject.AddComponent<Animator>();
            }
        }

    }
}
