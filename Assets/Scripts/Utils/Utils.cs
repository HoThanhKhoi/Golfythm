using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void FlipByScale(Transform transform, Vector2 initScale, bool flip)
    {
        if (flip)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else
        {
            transform.localScale = initScale;
        }
    }
}
