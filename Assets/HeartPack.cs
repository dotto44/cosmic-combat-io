using UnityEngine;
using Mirror;
using System;

public class HeartPack : NetworkBehaviour
{
    bool beenHit = false;
    public int pos = 0;
    [SerializeField] GameObject heartPopper;
    void Start()
    {
        pos = (int)Math.Round(gameObject.transform.parent.gameObject.transform.rotation.eulerAngles.z);
        if (!gameObject.transform.parent.GetComponent<PlasmaHolder>().getServer())
        {
            gameObject.transform.parent.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer)
        {
            return;
        }
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            var controller = hit.GetComponent<PlayerController>();
            if (controller == null || !beenHit)
            {
                return;
            }
            beenHit = true;

            if (health != null)
            {
                health.AddHealth(Damage.collisionWithAmt(50, collision.tag));
            }

            controller.CmdCallSpawnNewStar(pos);

            //NetworkServer.Destroy(gameObject.transform.parent.gameObject);
            // RpcEndThisPack();
            var parts = (GameObject)Instantiate(
                     heartPopper,
                       transform.position,
                      heartPopper.transform.rotation);
            controller.endPack(gameObject);
        }

    }
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {
            //  NetworkServer.Destroy(gameObject.transform.parent.gameObject);

            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            var controller = hit.GetComponent<PlayerController>();
            if(controller != null && controller.amILocalPlayer() && !isLocal)
             {
                //CONTINUE
            }
            else
            {
                return;
            }
            isLocal = true;
         
            if (health != null)
            {
                health.AddHealth(Damage.collisionWithAmt(50, collision.tag));
            }

                controller.CmdCallSpawnNewStar(pos);

            //NetworkServer.Destroy(gameObject.transform.parent.gameObject);
           // RpcEndThisPack();
               var parts = (GameObject)Instantiate(
                        heartPopper,
                          transform.position,
                         heartPopper.transform.rotation);
            controller.endPack(gameObject);
            //  NetworkServer.Spawn(parts);
            // NetworkServer.Destroy(gameObject.transform.parent.gameObject);
        }

    }*/

}
