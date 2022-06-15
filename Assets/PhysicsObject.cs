using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PhysicsObject : NetworkBehaviour
{
    public bool isInPlanetRange = false;
    public float minGroundNormalY = .65f;
    public float gravityModifier = 0f;

    private bool falling = false;

    public Transform feetPos;




    public GameObject[] planets;

    protected bool jetpacking = false;
    protected Vector2 targetVelocity;
    protected bool grounded;
    protected float groundedTimer;
    protected bool groundedTimerInUse = true;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected bool isOnPlanet;
    protected bool willBeOnPlanet;
    protected bool isOnPlatform;
    Vector2 platformPosition;
    Vector2 pastPlatformPosition;
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.02f;

    public bool shouldMove = true;
    protected bool isOnIce = false;
    protected bool dashing = false;
    void OnEnable()
    {

        rb2d = GetComponent<Rigidbody2D>();
    }

    public override void OnStartLocalPlayer()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        targetVelocity = Vector2.zero;
        ComputeVelocity();

    }

    protected virtual void ComputeVelocity()
    {

    }
    /*void OnTriggerEnter2D(Collider2D col)
	{
		if (!isLocalPlayer)
		{
			return;
		}
		//transform.SetParent(col.transform);
		//isInPlanetRange = true;
		//dist = Vector2.Distance (col.transform.position, transform.position);
	}
	/*void OnCollisionEnter (Collider other) 
	{
		shouldAttract = true;
	}
	void OnCollisionExit (Collider other) 
	{
		shouldAttract = false;
	} */
      void FixedUpdate()
      {
          if (!isLocalPlayer)
          {
              return;
          }

          
          falling = GameInputManager.GetKey("Drop");
          if (GameInputManager.GetKeyDown("Jump"))
          {
              groundedTimerInUse = false;
              grounded = false;
          }
          if (!GameInputManager.GetKey("Jump"))
          {
              groundedTimerInUse = true;
          }

          velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;


        
          velocity.x = targetVelocity.x;


          //grounded = false;
          if (velocity.y < -5 || velocity.y > 4)
          {
              groundedTimer++;
          }

          if (velocity.y <= -15)
          {
              velocity.y = -15;
          }


          if (groundedTimer > 3 || !groundedTimerInUse)
          {
              grounded = false;
          }

          Vector2 deltaPosition = velocity * Time.deltaTime;

          Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        //Vector2 moveAlongGround = new Vector2(1, 0);
          Vector2 move = moveAlongGround * deltaPosition.x;
          
          if (shouldMove)
          {
              if (!jetpacking)
              {
                  Movement(new Vector2(move.x, move.y), false);
              }
              else
              {
                  moveAlongGround = new Vector2(1, 0);
                  move = moveAlongGround * deltaPosition.x;
                  Movement(new Vector2(move.x, 0), false); ///0 here
              }
          }
          
          move = Vector2.up * deltaPosition.y;

          if (shouldMove)
          {
              Movement(new Vector2(move.x, move.y), true);
          }
    }

      void Movement(Vector2 move, bool yMovement)
      {

          float distance = move.magnitude;
          if (distance > minMoveDistance)
          {

              int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

              hitBufferList.Clear();

              for (int i = 0; i < count; i++)
              {
                  //if (hitBuffer [i].transform.name != "bullet(Clone)") {
                  if (hitBuffer[i].transform.tag != "Player" && hitBuffer[i].transform.tag != "BackgroundLayer" && hitBuffer[i].transform.tag != "Bullet" && hitBuffer[i].transform.tag != "NeptunianPlayer" && hitBuffer[i].transform.tag != "JuppernautPlayer" && hitBuffer[i].transform.tag != "RoverPlayer" && hitBuffer[i].transform.tag != "VenusianPlayer" && hitBuffer[i].transform.tag != "Platform" && hitBuffer[i].transform.tag != "MovingPlatform" && hitBuffer[i].transform.tag != "Icicle" ||
                      hitBuffer[i].transform.tag == "Platform" && hitBuffer[i].normal == Vector2.up && velocity.y <= 0 && yMovement && feetPos.position.y > hitBuffer[i].transform.position.y && !falling ||
                      hitBuffer[i].transform.tag == "Platform" && hitBuffer[i].normal == Vector2.up && isOnPlanet && grounded && !falling ||
                      hitBuffer[i].transform.tag == "MovingPlatform" && hitBuffer[i].normal == Vector2.up && isOnPlanet && grounded && !falling ||
                      hitBuffer[i].transform.tag == "MovingPlatform" && hitBuffer[i].normal == Vector2.up && velocity.y <= 0 && yMovement && feetPos.position.y > hitBuffer[i].transform.position.y && !falling)
                  {
                      hitBufferList.Add(hitBuffer[i]);

                      if (hitBuffer[i].collider.tag == "MoonGround")
                      {
                          willBeOnPlanet = true;
                          isOnIce = false;
                      }
                      else if (hitBuffer[i].collider.tag == "Ice")
                      {
                          willBeOnPlanet = true;
                          isOnIce = true;
                      }
                      else if (!isOnPlatform && hitBuffer[i].collider.tag == "MovingPlatform")
                      {
                          isOnIce = false;
                          isOnPlatform = true;
                        //transform.parent = hitBuffer[i].transform.parent.transform;
                        //Debug.Log("CALL1");
                        platformPosition = hitBuffer[i].transform.position;
                        pastPlatformPosition = platformPosition;

                    }
                  }
                  //}
              }
              isOnPlanet = willBeOnPlanet;

              willBeOnPlanet = false;
              if (isOnPlatform)
              {
                  if (hitBufferList.Count == 0)
                  {
                      isOnPlatform = false;
                      transform.parent = null;
                  }
                  else
                  {
                      bool remove = true;
                      foreach (RaycastHit2D hit in hitBufferList)
                      {
                          if (hit.collider.tag == "MovingPlatform")
                          {
                              remove = false;
                            //Debug.Log("CALL2");
                            pastPlatformPosition = platformPosition;
                            platformPosition = hit.transform.position;
                            move.x += 0.5f;
                          }
                      }
                      if (remove)
                      {
                          isOnPlatform = false;
                          transform.parent = null;
                      }
                  }

              }
              for (int i = 0; i < hitBufferList.Count; i++) //hitbufferlist.count
              {

                if (i > 0)
                {
                    if (hitBufferList[i].normal == new Vector2(0, 1))
                    {
                        continue;
                    }
                }

                Vector2 currentNormal = hitBufferList[i].normal;

               // Debug.Log(currentNormal.y);
                  if (currentNormal.y > minGroundNormalY)
                  {
                      grounded = true;
                      groundedTimer = 0;
                      

                    if (yMovement)
                      {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                      }

                  }

                  float projection = Vector2.Dot(velocity, currentNormal);
         
                  if (projection < 0)
                  {
                    velocity = velocity - projection * currentNormal;
                  }

                  float modifiedDistance = hitBufferList[i].distance - shellRadius;
                  distance = modifiedDistance < distance ? modifiedDistance : distance;

              }


          }

          rb2d.position = rb2d.position + move.normalized * distance;
         /* if (isOnPlatform)
          {
              rb2d.position = rb2d.position + (platformPosition - pastPlatformPosition) / 2;
          }*/
      }
   
    public void reverseVelocity()
    {
        groundNormal = new Vector2(0, 1);
        velocity.y = 20;
    }
    IEnumerator turnTimerOn()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("TURN ON");
        groundedTimerInUse = true;
    }
}

