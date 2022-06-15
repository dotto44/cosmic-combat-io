using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNormanBullet : LocalBulletBase
{
    private Color myColor = new Color(.99f, 0.99f, 0.35f);
   public override Color getColor()
    {
        return myColor;
    }
    public override short particleCount()
    {
        return 4;
    }
    public override bool specialParticles()
    {
        return false;
    }
    public void setColor()
    {
        myColor = new Color(0.486f, 0.76f, 0.96f);
    }
}
