#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerFireball : ServerBulletBase
{
    [SerializeField] GameObject fireballExp;
    void SpawnFireballExplosion(Vector2 pos, string WFM)
    {

        var explos = (GameObject)Instantiate(
                        fireballExp,
                        pos,
                        fireballExp.transform.rotation);
        NetworkServer.Spawn(explos);
        explos.GetComponent<ServerBulletBase>().whoFiredMe = WFM;


    }
    public override int damageAmt()
    {
        return 60;
    }

    public override int rockDamageAmt()
    {
        return 10;
    }
    public override void customDeathFunction()
    {
        SpawnFireballExplosion(transform.position, whoFiredMe);
    }
    public override string whatMethodOfDeath()
    {
        return "JUPPERNAUT - NORMAL";
    }
}
#endif