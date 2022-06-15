using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBulletRover : LocalBulletBase
{
 public override Color getColor()
    {
       return new Color((79f/255f), 1, (80f/255f));
    }
    public override short particleCount()
    {
        return 2;
    }
    public override bool specialParticles()
    {
        return false;
    }
}
