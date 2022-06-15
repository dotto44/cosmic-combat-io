#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerExplosion : ServerBulletBase
{
    int myDamage = 8;
    int myRockDamage = 5;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!isServer)
        {
            return;
        }
        checkHit(collision);
    }
    public override int damageAmt()
    {
        return myDamage;
    }

    public override int rockDamageAmt()
    {
        return myRockDamage;
    }
    public void allowAHit()
    {
        myDamage = 8;
        myRockDamage = 5;

    }
    public void allowAHitNo()
    {
        myDamage = 0;
        myRockDamage = 0;
    }
    public override bool canDie() { return false; }
    public override string whatMethodOfDeath()
    {
        return "JUPPERNAUT - EXPLOSION";
    }
    
}
#endif
