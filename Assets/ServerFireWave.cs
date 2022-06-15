#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerFireWave : ServerBulletBase
{
    public override int damageAmt()
    {
        return 30;
    }

    public override int rockDamageAmt()
    {
        return 3;
    }
    public override string whatMethodOfDeath()
    {
        return "NEPTUNIAN - NORMAL";
    }
}
#endif
