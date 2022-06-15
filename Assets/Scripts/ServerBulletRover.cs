#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBulletRover : ServerBulletBase
{
    public override int damageAmt()
    {
        return 25;
    }

    public override int rockDamageAmt()
    {
        return 2;
    }
    public override string whatMethodOfDeath()
    {
        return "ROVER - NORMAL";
    }
}
#endif
