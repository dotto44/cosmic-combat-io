using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlasmaHolder : NetworkBehaviour
{
    // Start is called before the first frame update
   public bool getServer()
    {
        return isServer;
    }
}
