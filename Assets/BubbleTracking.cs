#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BubbleTracking : MonoBehaviour
{
    List<GameObject> players;
    GameObject[] gos;
     [SerializeField] AIDestinationSetter aIDestinationSetter;
    [SerializeField] ServerBubble serverBubble;
    int closestPlayer = 0;
    float closestDistance = 999;
    public string playerWhoFired;
    public void begintracking()
    {
        players = new List<GameObject>();
        gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
        findClosestPlayer();
        if(System.Math.Abs(closestDistance - 999) > 0.01)
        {
            aIDestinationSetter.target = players[closestPlayer].transform;
            serverBubble.SetTarget(players[closestPlayer]);
        }
        else
        {
            serverBubble.SetTarget(null);
        }

    }
    public void findClosestPlayer()
    {
        foreach (GameObject go in gos)
        {
            Debug.Log(go.name);
            foreach(string s in ServerBulletBase.characterTypes)
            {
                if (go.CompareTag(s))
                {
                    players.Add(go);
                    break;
                }
            }
        }
      
        int count = 0;

    
        foreach (GameObject player in players)
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if (dist < closestDistance && player.name != playerWhoFired)
            {
                closestDistance = dist;
                closestPlayer = count;
            }

            count++;
        }
    }
   
 
}
#endif