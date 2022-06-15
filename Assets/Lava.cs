using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Lava : NetworkBehaviour
{
    [SerializeField] double damageAmt = 50;
    [SerializeField] bool knockBack = true;
    [SerializeField] GameObject damageText;
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {
            if (knockBack)
            {
                collision.gameObject.GetComponent<PhysicsObject>().reverseVelocity();
            }
            collision.gameObject.GetComponent<Health>().DealSelfDamage(Damage.collisionWithAmt(damageAmt, collision.tag), GetName.userName, "LAVA");
            Quaternion storingTextAsRotation = Quaternion.Euler(0, 0, (float)damageAmt);
            var damageTextInstance = (GameObject)Instantiate(
             damageText,
             Damage.generateDamageTextPosition(collision.gameObject),
             storingTextAsRotation);
            NetworkServer.Spawn(damageTextInstance);
        }
   
    }
  
}
