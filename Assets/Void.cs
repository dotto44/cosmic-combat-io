using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System.Collections;

public class Void : NetworkBehaviour
{
    [SerializeField] GameObject whiteCircle;
    [SerializeField] GameObject whiteCircleBlocker;

    [SerializeField] GameObject damageText;

    readonly float minX = -65; 
    readonly float maxX = 30; 
    readonly float minY = 15; 
    readonly float maxY = 33; 

    float distX; 
    float distY; 
    float distScale;
     
    int numberOfTicks; 
    int stormTicks; 
    bool shrinking; 
    int zoneNumber;

    Vector2[] centerPositions; 
    float[] sizes = { 6, 3, 1.5f, 0 }; 
    float sizeFct = 20; 
    int[] tickCounts = { 3000, 1000, 1800, 800, 1200, 600, 1000, 800 }; 
    int[] tickToShowCircle = { 1500, 1770, 1170, 970 }; 
    int[] damageValue = { 2, 2, 2, 4, 4, 7, 7, 11, 11 }; 

    List<Health> playersInVoid;

    #if UNITY_SERVER || UNITY_EDITOR
    private void Start()
    {
        playersInVoid = new List<Health>();
        centerPositions = new Vector2[4];

        // speedUpByFct(3);

        generateCirclePath();
        nextZone();
    }
    public void generateCirclePath()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        centerPositions[0] = new Vector2(x, y);

        centerPositions[1] = randomPointInCircle(sizes[0] * sizeFct / 2, sizes[1] * sizeFct / 2, centerPositions[0]);

        centerPositions[2] = randomPointInCircle(sizes[1] * sizeFct / 2, sizes[2] * sizeFct / 2, centerPositions[1]);

        centerPositions[3] = centerPositions[2];

    }
    public Vector2 randomPointInCircle(float radius, float smallRadius, Vector2 pos)
    {
        float len = Mathf.Sqrt(Random.Range(0.0f, 1.0f)) * (radius - smallRadius);
        float deg = Random.Range(0.0f, 1.0f) * 2.0f * Mathf.PI;
        float x = pos.x + len * Mathf.Cos(deg);
        float y = pos.y + len * Mathf.Sin(deg);
        Vector2 newPos = new Vector2(x, y);
        return newPos;
    }
    public void speedUpByFct(int fct)
    {
        for(int i = 0; i < tickCounts.Length; i++)
        {
            tickCounts[i] /= fct;
        }
        for (int i = 0; i < tickToShowCircle.Length; i++)
        {
            tickToShowCircle[i] /= fct;
        }
    }
    public void SetShrink(float scale, Vector2 pos)
    {
        Vector2 currentPosition = gameObject.transform.localPosition;

        distX = (currentPosition.x - pos.x) / numberOfTicks;
        distY = (currentPosition.y - pos.y) / numberOfTicks;
        distScale = (gameObject.transform.localScale.x - scale) / numberOfTicks;
    }
    public void nextZone()
    {
        if (zoneNumber == 8) return;

        numberOfTicks = tickCounts[zoneNumber];

        if (zoneNumber % 2 == 0)
        {
            shrinking = false;
        }
        else
        {
            shrinking = true;
            SetShrink(sizes[zoneNumber / 2], centerPositions[zoneNumber / 2]);
        }

        RpcSetShrinkOnClient(distScale, distX, distY, shrinking, numberOfTicks, zoneNumber);

        zoneNumber++;
    }
    public void addPlayerToStorm(Health player)
    {
        playersInVoid.Add(player);
    }
    public void removePlayerFromStorm(Health player)
    {
        playersInVoid.Remove(player);
    }
    public void dealDamageToPlayer(Health player)
    {
        double damageAmt = Damage.collisionWithAmt(damageValue[zoneNumber], player.gameObject.tag, player.gameObject.GetComponent<Health>().getExtraHealth());
        player.TakeDamage(damageAmt, player.gameObject.name, null, "Void");
        Quaternion storingTextAsRotation = Quaternion.Euler(0, 0, (float)damageAmt);
        var damageTextInstance = (GameObject)Instantiate(
         damageText,
         Damage.generateDamageTextPosition(player.gameObject),
         storingTextAsRotation);
        NetworkServer.Spawn(damageTextInstance);
    }
#endif
    [ClientRpc]
    void RpcSetShrinkOnClient(float distScale, float distX, float distY, bool shrinking, int numberOfTicks, int zoneNumber)
    {
        this.distScale = distScale;
        this.distX = distX;
        this.distY = distY;
        this.shrinking = shrinking;
        this.numberOfTicks = numberOfTicks;
        this.zoneNumber = zoneNumber;
    }
    [ClientRpc]
    void RpcSetWhiteCircle(float scale, Vector2 pos)
    {
        SetWhiteCircle(scale, pos);
    }
    public void SetWhiteCircle(float scale, Vector2 pos)
    {
        float circleBlockerScale = (scale > 0.1) ? scale - 0.15f : 0;
        whiteCircle.transform.localScale = new Vector3(scale, scale);
        whiteCircleBlocker.transform.localScale = new Vector3(circleBlockerScale, circleBlockerScale);
        whiteCircleBlocker.transform.localPosition = pos;
        whiteCircle.transform.localPosition = pos;
    }
    void FixedUpdate()
    {

        if (numberOfTicks > 0)
        {
            if (shrinking)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - distScale, gameObject.transform.localScale.y - distScale);
                gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x - distX, gameObject.transform.localPosition.y - distY);
            }
            #if UNITY_SERVER || UNITY_EDITOR
            else if (numberOfTicks == tickToShowCircle[zoneNumber / 2])
            {   
                SetWhiteCircle(sizes[zoneNumber / 2], centerPositions[zoneNumber / 2]);
                RpcSetWhiteCircle(sizes[zoneNumber / 2], centerPositions[zoneNumber / 2]);
            }
            #endif

            numberOfTicks--;

        }
        #if UNITY_SERVER || UNITY_EDITOR
        else if (numberOfTicks == 0)
        {
            nextZone();
            numberOfTicks--;
        }

        if(stormTicks == 0)
        {
            for(int i = 0; i < playersInVoid.Count; i++)
            {
                try {
                    dealDamageToPlayer(playersInVoid[i]);
                } catch(MissingReferenceException m)
                {
                    playersInVoid.RemoveAt(i);
                }
            }
            stormTicks = 35;
        }
        else
        {
            stormTicks--;
        }
        
        #endif

    }
}
