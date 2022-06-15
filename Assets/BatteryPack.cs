
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class BatteryPack : NetworkBehaviour
{
#if UNITY_SERVER || UNITY_EDITOR
    void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        health.addExtraHealth(15);
        NetworkServer.Destroy(gameObject);
    }
#endif
}
