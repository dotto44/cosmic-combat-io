using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFireball : LocalBulletBase
{
    public override Color getColor()
    {
        return new Color(241f / 255f, 158f / 255f, 206f / 255f);
    }
    public override short particleCount()
    {
        return 0;
    }
    public override bool specialParticles()
    {
        return true;
    }
}
