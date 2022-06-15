using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBigField : MonoBehaviour
{
    [SerializeField] Icicle icicle;
    int numberOfPlayersInRange;

    public void isNotServer()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, col.tag))
        {
            numberOfPlayersInRange++;
            icicle.inFarRange();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, col.tag))
        {
            numberOfPlayersInRange--;
            if(numberOfPlayersInRange == 0)
            {
                icicle.exitFarRange();
            }
        }
    }
}
