using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class throwingArm : NetworkBehaviour
{

    public GameObject jupParticles;
    [SerializeField] SpriteRenderer armRenderer2;
    [SerializeField] GameObject arm2;
    public void updateJupsSuper()
    {
        gameObject.transform.parent.gameObject.GetComponent<PlayerController>().stopJupSpecial();
    }
    public void fireGrenade()
    {
       
            gameObject.transform.parent.gameObject.GetComponent<PlayerController>().fireAGrenade(gameObject.transform.position);

    }
    public void flipArm()
    {
        if (isLocalPlayer)
        {
            return;
        }
        if (armRenderer2 == null)
        {
            armRenderer2 = arm2.GetComponent<SpriteRenderer>();
        }
        if (transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            Vector3 pos2 = arm2.transform.localPosition;
            pos2.x = 0.87f;
            arm2.transform.localPosition = pos2;
            armRenderer2.flipX = true;
        }
        else
        {
            Vector3 pos2 = arm2.transform.localPosition;
            pos2.x = -0.87f;
            arm2.transform.localPosition = pos2;
            armRenderer2.flipX = false;
        }
    }
    public void activateParticles()
    {
        GameObject particlePrefab = Instantiate(jupParticles);
        particlePrefab.transform.parent = gameObject.transform.parent.transform;
        particlePrefab.transform.localPosition = new Vector3(0, 0.93f, 0);
    }
}
