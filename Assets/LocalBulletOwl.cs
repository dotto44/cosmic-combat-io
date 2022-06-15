using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBulletOwl : LocalBulletBase
{
    [SerializeField] GameObject owlFeathers;
    public override Color getColor()
    {
        return new Color(241f / 255f, 158f / 255f, 206f / 255f);
    }
    public override short particleCount()
    {
        return 4;
    }
    public override bool specialParticles()
    {
        var bullet = (GameObject)Instantiate(
                         owlFeathers,
                         transform.position,
                         owlFeathers.transform.rotation);
        return true;
    }
}
