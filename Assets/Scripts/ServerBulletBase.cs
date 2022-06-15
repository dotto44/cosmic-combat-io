
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class ServerBulletBase : NetworkBehaviour
{
    public static string[] characterTypes = { "Player", "RoverPlayer", "VenusianPlayer", "JuppernautPlayer", "NeptunianPlayer" };
    #if UNITY_SERVER || UNITY_EDITOR
    [SerializeField] GameObject damageText;
    public abstract int damageAmt();
    public abstract int rockDamageAmt();
  
    public string whoFiredMe = "";
    public abstract string whatMethodOfDeath();

  
    public static string[] movingCollisions = { "" };

    public virtual void customDeathFunction() { }
    public virtual bool canDie() { return true; }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer)
        {
            return;
        }
        checkHit(collision);
    }

    public void checkHit(Collider2D collision)
    {
        var hit = collision.gameObject;
        if (collision.tag != "Detector" && !collision.name.Contains("Bubble"))
        {
            if (isACharacter(collision.tag))
            {
                dealDamageToPlayer(collision, hit);
            }
            else
            {
                if (collision.tag == "Boulder")
                {
                    hit.transform.parent.gameObject.GetComponent<Boulder>().hitRock(rockDamageAmt());
                }
                if(collision.tag == "Crate")
                {
                    hit.GetComponent<Crate>().hit();
                }
                if (collision.tag == "Icicle")
                {
                    hit.GetComponent<Icicle>().destroyIcicle();
                }
                if (canDie()) { killBullet(); }
               
            }
           

        }
    }
    public bool isACharacter(string tag)
    {
        foreach (string s in characterTypes)
        {
            if (tag.Equals(s))
            {
                return true;
            }
        }
        return false;
    }
    private Vector2 generateDamageTextPosition(GameObject hit)
    {
        CapsuleCollider2D capsule = hit.GetComponent<CapsuleCollider2D>();
        float randomX = Random.Range(-capsule.size.x/2, capsule.size.x / 2);
        float randomY = Random.Range(0.15f, capsule.size.y/2);
        return new Vector2(hit.transform.position.x + randomX, hit.transform.position.y + randomY);
    }
    public void dealDamageToPlayer(Collider2D collision, GameObject hit)
    {

        var health = hit.GetComponent<Health>();
        Debug.Log("Hit Name: " + hit.name + "WhoFired: " + whoFiredMe);
        if (!whoFiredMe.Equals(hit.name))
        { 
        Debug.Log("Damge Dealing");
            if (canDie()) { killBullet(); }
            if (health != null)
            {
                health.TakeDamage(Damage.collisionWithAmt(damageAmt(), collision.tag, health.getExtraHealth()), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe, whatMethodOfDeath());
                //var damageText = Instantiate(Resources.Load("DamageText", typeof(GameObject))) as GameObject;
                //damageText.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y + 5);
                if (damageAmt() != 0)
                {
                    Quaternion storingTextAsRotation = Quaternion.Euler(0, 0, damageAmt());
                    var damageTextInstance = (GameObject)Instantiate(
                     damageText,
                     generateDamageTextPosition(hit),
                     storingTextAsRotation);
                    // damageTextInstance.GetComponent<DamageText>().setText("" + damageAmt());
                    NetworkServer.Spawn(damageTextInstance);
                }
            }
        }

    }
    
    public void killBullet()
    {
        customDeathFunction();
        Destroy(gameObject);
    }
    #endif
}
