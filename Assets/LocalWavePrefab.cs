using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalWavePrefab : LocalBulletBase
{
    bool localCanDie = false;
    [SerializeField] GameObject fishy;
    public override Color getColor()
    {
        return new Color(75f / 255f, 201f / 255f, 212f / 255f);
    }
    public override short particleCount()
    {
        return 4;
    }
    public override bool specialParticles()
    {
        if (isACharacter(getTag()))
        {
            localCanDie = false;
            return false;
        }
        else
        {
            localCanDie = true;
            return true;
        }

    }

    public override void specialParticlesContent(Collider2D collider)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.GetChild(0).gameObject.transform.position.y, 0);
       
        var pellet1 = (GameObject)Instantiate(
             fishy,
             pos,
             transform.GetChild(0).gameObject.transform.rotation);

        Destroy(pellet1, 0.5f);

    }

    public override bool canDie()
    {
        return true;
    }
}
