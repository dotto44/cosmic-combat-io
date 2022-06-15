using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float movementFactor;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {
            collision.gameObject.GetComponent<PlayerController>().setGroundMovementSpeed(movementFactor);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {
            collision.gameObject.GetComponent<PlayerController>().setGroundMovementSpeed(0);
        }
    }
}
