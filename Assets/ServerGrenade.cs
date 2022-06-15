#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerGrenade : NetworkBehaviour
{
    [SerializeField] GameObject fireballExp;
    public string whoFiredMe = "";
    public void checkHit(Collider2D collision)
    {
        var hit = collision.gameObject;
        if (collision.tag != "Detector")
        {
            if (isACharacter(collision.tag))
            {
                dealDamageToPlayer(collision, hit);
            }
        }
    }
    public bool isACharacter(string tag)
    {
        foreach (string s in ServerBulletBase.characterTypes)
        {
            if (tag.Equals(s))
            {
                return true;
            }
        }
        return false;
    }
    public void dealDamageToPlayer(Collider2D collision, GameObject hit)
    {



        if (!whoFiredMe.Equals(hit.name))
        {
            grenadeExploded();
        }

    }
    void SpawnFireballExplosion(Vector2 pos, string WFM)
    {

        var explos = (GameObject)Instantiate(
                        fireballExp,
                        pos,
                        fireballExp.transform.rotation);
        NetworkServer.Spawn(explos);
        explos.GetComponent<ServerBulletBase>().whoFiredMe = WFM;
       

    }
    public void grenadeExploded()
    {
        if (!isServer)
        {
            return;
        }
        else
        {
            SpawnFireballExplosion(gameObject.transform.position, whoFiredMe);
        }
        NetworkServer.Destroy(gameObject);
    }
  
}
#endif