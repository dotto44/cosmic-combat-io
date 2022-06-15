using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalBulletBase : MonoBehaviour
{
    /*private static Color[] moonColors = {new Color(235, 232, 222), new Color(213, 210, 201), new Color(184, 181, 169)};
    private static Dictionary<string, Color[]> explosionColors = new Dictionary<string, Color[]>(){
                                                {"MoonGround", moonColors},
                                            };
    private Color[] myColors;*/
    public abstract Color getColor();
    public abstract short particleCount();
    public abstract bool specialParticles();
    public virtual bool canDie() { return true; }
    public virtual void specialParticlesContent(Collider2D collider) { }
    private string hitTag;
    public string getTag()
    {
        return hitTag;
    }
    [SerializeField] GameObject explosionPrefab;

    public string whoShot = "";

    void OnTriggerEnter2D(Collider2D collision)
    {
        checkHit(collision);
    }


    public void deathParticles(string tag, Collider2D collision)
    {

        //  myColors = explosionColors[tag];

        //  pellet1.GetComponent<BulletParticlesFixed>().AssignGradient(myColors);
        bool specialCase = specialParticles();
        if (!specialCase)
        {
            Vector2 dir = transform.GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion partRot = Quaternion.AngleAxis(angle, Vector3.forward);
            //   partRot =  Quaternion.Euler(partRot.x, partRot.y, partRot.z);
            var pellet1 = (GameObject)Instantiate(
               explosionPrefab,
               transform.position,
               partRot);
            pellet1.GetComponent<BulletParticlesFixed>().AssignGradient(getColor(), particleCount());
        }
        else
        {
            specialParticlesContent(collision);
        }
    }
    public void checkHit(Collider2D collision)
    {
        var hit = collision.gameObject;
        if (collision.tag != "Detector" && !whoShot.Equals(hit.name))
        {
            hitTag = collision.tag;
            deathParticles(hit.tag, collision);
            if (canDie()) { Destroy(gameObject); }
        }
    }
    public bool isACharacter(string tag)
    {
        foreach (string s in ServerBulletBase.characterTypes)
        {
            if (tag.Equals(s))
            {
                return true;
            }
        }
        return false;
    }
}
