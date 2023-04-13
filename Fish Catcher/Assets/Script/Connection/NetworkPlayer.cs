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
        }
        else
        {
            Debug.Log("[CUSTOM MESSAGE] Spawned Other Player");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
