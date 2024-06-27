using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;

    public void AddToTarget(Transform objectToAdd, float weigh, float radius)
    {
        if (targetGroup.FindMember(objectToAdd) > 0) { return; }
        targetGroup.AddMember(objectToAdd, weigh, radius);
    }

    public void RemoveFromTarget(Transform objectToRemove)
    {
        targetGroup.RemoveMember(objectToRemove);
    }


}
