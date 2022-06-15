using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Crown : NetworkBehaviour
{
    private string state = "FLOATING"; //FLOATING, BOUNCING, CAPTURED
    int[] boundaries;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] Animator animator;

    public override void OnStartServer()
    {
        base.OnStartServer();
        setBoundaries();
    }
    public void setBoundaries()
    {
        boundaries = GameObject.FindWithTag("SpawnPointManager").GetComponent<MapInfo>().getBoundaries();
    }
    void Update()
    {
        checkOOB();
    }

    private void launchCrownWithForce(float xDirection, float yDirection, float thrust)
    {
        rigidbody2D.AddForce(new Vector2(xDirection, yDirection).normalized * thrust, ForceMode2D.Impulse);
    }
    private void launchCrownWithForce(float thrust)
    {
        float xDirection = Random.Range(-1, 1);
        float yDirection = Random.Range(0.3f, 1);
        rigidbody2D.AddForce(new Vector2(xDirection, yDirection).normalized * thrust, ForceMode2D.Impulse);
    }
    public void setState(string state)
    {
        this.state = state;
        animator.CrossFade("Crown", 0.0f);
        if (state == "FLOATING")
        {
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.SetRotation(0);
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetInteger("state", 1);
        }
    }
    public void updateMapIcon()
    {

    }
    public void respawn()
    {
        setState("FLOATING");
        gameObject.transform.position = new Vector2(0, 0);
    }

    public void checkOOB()
    {
        if (gameObject.transform.position.y < boundaries[2] || gameObject.transform.position.y > boundaries[3] || gameObject.transform.position.x > boundaries[1] || gameObject.transform.position.x < boundaries[0])
        {
            respawn();
        }
    }
    IEnumerator waitAndRespawn(int time)
    {
        yield return new WaitForSeconds(time);
        respawn();
    }
}