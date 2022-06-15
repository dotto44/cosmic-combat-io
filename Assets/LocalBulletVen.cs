using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBulletVen : LocalBulletBase
{
    bool localCanDie = false;
    [SerializeField] GameObject venArrow;
    public override Color getColor()
    {
        return new Color(241f/255f, 158f/255f, 206f/255f);
    }
    public override short particleCount()
    {
        return 4;
    }
    public override bool specialParticles()
    {
        if(isACharacter(getTag()))
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
        var pellet1 = (GameObject)Instantiate(
             venArrow,
             transform.position,
             transform.rotation);
      
        Destroy(pellet1, 0.5f);
       
    }

    public override bool canDie()
    {
        return true;
    }
}
