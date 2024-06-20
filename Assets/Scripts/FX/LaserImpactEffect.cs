using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserImpactEffect : MonoBehaviour
{
    private void SetUnActive()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(UnActiveAfter(0.375f));
    }

    private IEnumerator UnActiveAfter(float time)
    {
        yield return new WaitForSeconds(time);
        SetUnActive();
    }
}
