#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBulletOwl : ServerBulletBase
{
    public override int damageAmt()
    {
        return 35;
    }

    public override int rockDamageAmt()
    {
        return 4;
    }
    public override string whatMethodOfDeath()
    {
        return "VENUSIAN - OWL";
    }
}
#endif
