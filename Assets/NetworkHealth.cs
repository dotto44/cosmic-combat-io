using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetworkHealth : NetworkBehaviour
{
    [Command]
    public void CmdServerDealWithPlayerHit(int amount, string whosDead, string whoShot)
    {
        Debug.Log("CALLED");
      //  GameObject.Find(whosDead).GetComponent<Health>().TakeDamage(amount, whosDead, whoShot);
    }
}
