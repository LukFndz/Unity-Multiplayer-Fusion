using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; set; }
    public override void Spawned()
    {
        if(Object.HasInputAuthority)
        {
            Local = this;

            Debug.Log("[CUSTOM MESSAGE] Spawned Own Player");
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("[CUSTOM MESSAGE] Spawned Other Player");
            Camera localCamera = GetComponentInChildren<Camera>(true);
            localCamera.enabled = false;

            AudioListener localListener = GetComponentInChildren<AudioListener>(true);
            localListener.enabled = false;
        }

    }

}
