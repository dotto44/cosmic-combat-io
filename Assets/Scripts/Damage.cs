using UnityEngine;
using System.Collections.Generic;

public static class Damage
{
    public static Dictionary<string, int> healthValuesByTag = new Dictionary<string, int>()
        {
            { "Player", 100 },
            { "RoverPlayer", 125 },
            { "VenusianPlayer", 80 },
            { "JuppernautPlayer", 200 },
            { "NeptunianPlayer", 150 },
        };
    public static double collisionWithAmt(double amtOfDamage, string playerTag)
    {
        return amtOfDamage; // healthValuesByTag[playerTag] * 100;
    }
    public static double collisionWithAmt(double amtOfDamage, string playerTag, double extraHealth)
    {
        return amtOfDamage;  // (extraHealth + healthValuesByTag[playerTag]) * 100;
    }
        public static Vector2 generateDamageTextPosition(GameObject hit)
    {
        CapsuleCollider2D capsule = hit.GetComponent<CapsuleCollider2D>();
        float randomX = Random.Range(-capsule.size.x / 2, capsule.size.x / 2);
        float randomY = Random.Range(0.15f, capsule.size.y / 2);
        return new Vector2(hit.transform.position.x + randomX, hit.transform.position.y + randomY);
    }

}
