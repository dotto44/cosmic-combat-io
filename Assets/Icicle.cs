using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Icicle : NetworkBehaviour
{
    [SerializeField] GameObject damageText;
    [SerializeField] Animator icicleAnim;
    [SerializeField] AudioSource audioData;
    [SerializeField] GameObject pieces;
    [SerializeField] GameObject cielingPieces;
    [SerializeField] Rigidbody2D rigidbody;
    bool frozen;
    Vector2 startingPosition;
    private void Start()
    {
        if (!isServer)
        {
            gameObject.GetComponentInChildren<IcicleBigField>().isNotServer();
            gameObject.GetComponentInChildren<IcicleFallField>().isNotServer();
            gameObject.GetComponentInChildren<IciclePlayerCollision>().isNotServer();
        }
            startingPosition = transform.position;
    }
    public void spawnPieces()
    {
         if (isServer)
         {
            transform.position = startingPosition;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
         }
         GameObject explosion = Instantiate(pieces, gameObject.transform);
         explosion.transform.parent = null;
    }
    public void inFarRange()
    {
        icicleAnim.SetBool("slightShake", true);
    }
    public void inFallRange()
    {
        icicleAnim.SetBool("bigShake", true);
    }
    public void fall()
    {
        if (isServer)
        {
            rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }
    public void exitFarRange()
    {
        icicleAnim.SetBool("slightShake", false);
    }
    public void exitFallRange()
    {
        icicleAnim.SetBool("bigShake", false);
    }
    private IEnumerator yah()
    {
        yield return new WaitForSeconds(10);
        yeet();
    }
    public void resetPosition()
    {
         if (isServer)
        {
            icicleAnim.SetBool("reset", false);
            icicleAnim.SetBool("quickDestroy", false);
        }
       
    }
    private void yeet()
    {
        icicleAnim.SetBool("reset", true);
       // destroyed = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (isServer)
        {
            destroyIcicle();
        }
    }
    
    public void handleTrigger(Collider2D collision)
    {
        if (!isServer)
        {
            return;
        }
        if (icicleAnim.GetCurrentAnimatorStateInfo(0).IsName("IcicleInvisible"))
        {
            return;
        }
        if (MethodResource.arrayContains(ServerBulletBase.characterTypes, collision.tag))
        {
            dealIcicleDamageToPlayer(collision.gameObject, collision);
        }
        destroyIcicle();
    }
    public void destroyIcicle()
    {
            //destroyed = true;
            //icicleAnim.SetBool("bigShake", false);
            icicleAnim.SetBool("quickDestroy", true);
            //icicleAnim.SetBool("slightShake", false);
            StartCoroutine(yah());
    }
    private void dealIcicleDamageToPlayer(GameObject hit, Collider2D collision)
    {
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(Damage.collisionWithAmt(30, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, null, "ICICLE");
            Quaternion storingTextAsRotation = Quaternion.Euler(0, 0, 30);
            var damageTextInstance = (GameObject)Instantiate(
             damageText,
             Damage.generateDamageTextPosition(hit),
             storingTextAsRotation);
            // damageTextInstance.GetComponent<DamageText>().setText("" + damageAmt());
            NetworkServer.Spawn(damageTextInstance);
        }
    }
}
