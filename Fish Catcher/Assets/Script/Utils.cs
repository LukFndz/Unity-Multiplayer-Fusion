using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
   public static void SetRenderLayoutInChildren(Transform transform, int layerNumber)
    {
        foreach (Transform item in transform.GetComponentInChildren<Transform>(true))
        {
            item.gameObject.layer = layerNumber;
        }
    }
}
