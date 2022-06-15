using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
public class PlasmaPack : NetworkBehaviour
{
    public int positionNum;
    [SerializeField] GameObject plasmaPopper;
    // Use this for initialization
    void Start()
    {
        if (!gameObject.transform.parent.GetComponent<PlasmaHolder>().getServer())
        {
            Debug.Log("OPENED");
            
            positionNum = (int)Math.Round(gameObject.transform.parent.gameObject.transform.rotation.eulerAngles.z);
            Debug.Log(gameObject.transform.parent.gameObject.transform.rotation.eulerAngles.z);
            Debug.Log(positionNum);
            gameObject.transform.parent.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
      
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("THE POSITION HERE IS:" + positionNum);
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {

           // NetworkServer.Destroy(gameObject.transform.parent.gameObject);

        var hit = collision.gameObject;
            var controller = hit.GetComponent<PlayerController>();
            if (controller != null && controller.isLocalPlayer)
            {
                //Continue
            }
            else
            {
                return;
            }
            var parts = (GameObject)Instantiate(
                         plasmaPopper,
                           transform.position,
                          plasmaPopper.transform.rotation);
            controller.endPack(gameObject);
            controller.refillFuelLocalPlayerAuthority(positionNum);

        }

    }
}
