#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerBubble : ServerBulletBase
{
    [SerializeField] BubbleTracking bubble;
    [SerializeField] GameObject poppingBubble;
    Animator checkForServer;
    bool hasSetTarget;
    float timeAtStart = 0;
    bool shouldKillASAP;
    
    void Start()
    {
        if (!isServer)
        {
            return;
        }
        timeAtStart = Time.time;
        bubble.playerWhoFired = whoFiredMe;
        bubble.begintracking();
    }
    private void Update()
    {
        if (!isServer || !hasSetTarget)
        {
            return;
        }
        if (shouldKillASAP || checkForServer.GetBool("dead") && Time.time - timeAtStart > 1f || Time.time - timeAtStart > 6f)
        {
            killBullet();
        }
    }
    public void SetTarget(GameObject target)
    {
        if(target != null)
        {
            checkForServer = target.GetComponent<Animator>();
            hasSetTarget = true;
        }
        else
        {
            hasSetTarget = true;
            shouldKillASAP = true;
        }

    }
    public override int damageAmt()
    {
        return 20;
    }

    public override int rockDamageAmt()
    {
        return 2;
    }
    public override string whatMethodOfDeath()
    {
        return "NEPTUNIAN - SUPER";
    }
    private void OnDestroy()
    {
       
        Instantiate(
         poppingBubble,
         transform.position,
         Quaternion.Euler(0,0,0));
         
    }
}
#endif