using System.Collections;
using UnityEngine;
using Mirror;
public class Crate : NetworkBehaviour
{
   
    private int rockStage = 6;

    public float stageResetValue = 0;
    [SerializeField] Animator boulderAnim;
    [SerializeField] GameObject item;
    [SerializeField] GameObject pieces;
    bool frozen;
    [ClientRpc]
    public void RpcSpawnPieces()
    {
        if (isServer)
        {
            return;
        }
        Instantiate(pieces, gameObject.transform);
    }
    public void hit()
    {
        if (!isServer || frozen)
        {
            return;
        }
        rockStage--;
        if (rockStage <= stageResetValue)
        {
            frozen = true;
            boulderAnim.SetInteger("stage", 7);
            rockStage = 7;
            RpcSpawnPieces();
            var battery = (GameObject)Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
            NetworkServer.Spawn(battery);
            // StartCoroutine(yah()); Don't respawn
        }
        else
        {
            boulderAnim.SetInteger("stage", rockStage);
        }
    }
   
    private IEnumerator yah()
    {
        yield return new WaitForSeconds(25);
        StartCoroutine(yeet());
    }
    private IEnumerator yeet()
    {
        bool shouldRespawn = false;
        while (!shouldRespawn)
        {
            Debug.Log("STARTING CHECK FOR RESPAWN");
            yield return new WaitForSeconds(5.0f);
            shouldRespawn = ScanForItems();
        }
        boulderAnim.SetInteger("stage", 6);
        rockStage = 6;
        frozen = false;
        Debug.Log("RESPAWN");
    }

    private bool ScanForItems()
    {
        Debug.Log("SCANNING");
        bool returnType = true;
        Collider2D[] allOverlappingColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 7);
        foreach (Collider2D hit in allOverlappingColliders)
        {
            if (MethodResource.arrayContains(ServerBulletBase.characterTypes, hit.tag))
            {
                
                returnType = false;
                Debug.Log("FOUND PLAYER");
            }
        }
        Debug.Log("DIDNT FIND PLAYER");
        return returnType;
    }
}
