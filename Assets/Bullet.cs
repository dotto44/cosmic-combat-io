//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;

//public class Bullet : NetworkBehaviour {
//	public string whoFiredMe = "";


//	public GameObject objectWhoFiredMe;
//	private float TimeAlive = 0.0f;

//	public int bulletType = 1;
//    public bool isRoverType1 = false;
//	public bool isOwl = false;
//    bool shouldGiveDamage = false;
//    bool hasSpawnedFire = false;
//    [SerializeField] GameObject owlFeathers;
//    [SerializeField] GameObject fireballExp;


//    bool canDamageRock = true;
//      void Start(){

//		TimeAlive = Time.deltaTime;
//		//Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), GetComponent<Collider>());
//	}
//    public void allowAHit()
//    {
//        shouldGiveDamage = true;
    
//    }
//    public void allowAHitNo()
//    {
//        shouldGiveDamage = false;
//    }

//    void OnTriggerStay2D(Collider2D collision)
//    {
      
//        if (bulletType == 4 && shouldGiveDamage)
//        {
//            Debug.Log("STAYING CAPIATL 4");
//            var hit = collision.gameObject;
//            Debug.Log(collision.tag);
//            if (collision.tag != "Bullet" && collision.tag != "JuppernautPlayer" && collision.tag != "Player" && collision.tag != "RoverPlayer" && collision.tag != "VenusianPlayer" && collision.tag != "Detector")
//            {
//                if (collision.tag == "Boulder" && canDamageRock && isServer)
//                {
//                    Debug.Log("Staying 3");
//                    int amount = 5;
                    

//                    hit.transform.parent.gameObject.GetComponent<Boulder>().hitRock(amount);

//                }
              
//            }

//            if (collision.tag == "Player" || collision.tag == "JuppernautPlayer" || collision.tag == "RoverPlayer" || collision.tag == "VenusianPlayer")
//            {
//                var health = hit.GetComponent<Health>();
//                Debug.Log("Staying 4");
//                if (whoFiredMe != hit.name)
//                {
//                    Debug.Log("Staying 5");
//                    //NetworkServer.Spawn (blood);
//                    //Health health = hit.GetComponent<Health>();

//                    if (health != null)
//                    {
//                        //  Debug.Log ("DAMAGE");
//                        //  Debug.Log (whoFiredMe);
//                        //  Debug.Log (hit.name);

//                        Debug.Log("Staying 6");

//                        if (isServer)
//                        {
//                            health.TakeDamage(collisionWithAmt(8, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                        }
//                    }
//            }
//            //}
//    }
//    }
//    void OnTriggerEnter2D(Collider2D collision)
//	{
       
//        //if (Time.deltaTime - TimeAlive > 0.05) {
//        var hit = collision.gameObject;
//      if(bulletType == 5)
//        {
//            Debug.Log("QUIT OUT");
//            return;
//        }
//        if (bulletType == 4)
//        {
//            Debug.Log("ENETRING 4");
//        }
//        if (collision.tag != "Bullet" && collision.tag != "Player" && collision.tag != "JuppernautPlayer" && collision.tag != "RoverPlayer" && collision.tag != "VenusianPlayer"  && collision.tag != "Detector") {
//			if (collision.tag == "Boulder") {
//				int amount = 0;
//				if (bulletType == 1) {
//					amount = 2;
//				}
//				if (bulletType == 2) {
//					amount = 3;
//				}
//                if (bulletType == 3)
//                {
//                amount = 10;
//                }
//                if (bulletType == 4)
//                {
//                    amount = 5;

//                }
//                if (isServer && canDamageRock)
//                {
//                    hit.transform.parent.gameObject.GetComponent<Boulder>().hitRock(amount);
//                }
//            }

//            if (!isOwl) {
//                if (bulletType != 4)
//                {
//                    killBullet();
//                }
//			} else {
//				killBulletOwl ();
//			}
//		}

//		if (collision.tag == "Player" || collision.tag == "JuppernautPlayer" || collision.tag == "RoverPlayer"  || collision.tag == "VenusianPlayer") {
//			var health = hit.GetComponent<Health> ();
          
//            if (whoFiredMe != hit.name) {
//				//NetworkServer.Spawn (blood);
//				//Health health = hit.GetComponent<Health>();
//				if (!isOwl) {

//                    if (bulletType != 4)
//                    {
//                        killBullet();
//                    }
//                } else {
//					killBulletOwl ();
//				}
//				if (health != null) {
//				//	Debug.Log ("DAMAGE");
//				//	Debug.Log (whoFiredMe);
//				//	Debug.Log (hit.name);
//					if (bulletType == 1) {
//                        /*	if (collision.tag == "Player") {
//                                health.TakeDamage (25, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//                            } else {
//                                if (collision.tag == "RoverPlayer") {
//                                    health.TakeDamage (20, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//                                } else {
//                                    health.TakeDamage (32, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//                                }
//                            }*/
//                        Debug.Log("Staying 6");
//                        if (isServer  && isRoverType1)
//                        {
//                            health.TakeDamage(collisionWithAmt(25, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                        if (isServer && !isRoverType1)
//                        {
//                            health.TakeDamage(collisionWithAmt(25, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                    } else if(bulletType == 2){

//                        /*if (collision.tag == "Player") {
//							health.TakeDamage (30, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//						} else {
//							if (collision.tag == "RoverPlayer") {
//								health.TakeDamage (24, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//							} else {
//								health.TakeDamage (38, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//							}
//						}*/
//                        Debug.Log("Staying 6");
//                        if (!isServer && GetName.userName == whoFiredMe && isOwl)
//                        {
//                            health.TakeDamage(collisionWithAmt(30, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                        if (isServer && !isOwl)
//                        {
//                            health.TakeDamage(collisionWithAmt(30, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                        } else if(bulletType == 3)
//                    {
//                        if (!isServer && GetName.userName == whoFiredMe)
//                        {
//                            health.TakeDamage(collisionWithAmt(50, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                    }
//                    else
//                    {
//                        Debug.Log("Explosion Damage");
//                        if (isServer)
//                        {
//                            health.TakeDamage(collisionWithAmt(8, collision.tag), collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//                        }
//                     }
//                }
//			} 
//		}
//		//}
//	} 
//	// Use this for initialization
///*	void OnCollisionEnter2D(Collision2D collision)
//	{
//		//if (Time.deltaTime - TimeAlive > 0.05) {
//			var hit = collision.gameObject;

//		if (collision.collider.tag != "Bullet" && collision.collider.tag != "Player" && collision.collider.tag != "RoverPlayer" && collision.collider.tag != "VenusianPlayer") {
//				Destroy (gameObject);
//			}

//		if (collision.collider.tag == "Player" || collision.collider.tag == "RoverPlayer"  || collision.collider.tag == "VenusianPlayer") {
//				var health = hit.GetComponent<Health> ();
			  
//			if (whoFiredMe != hit.name) {
//				//NetworkServer.Spawn (blood);
//				//Health health = hit.GetComponent<Health>();
//				Destroy (gameObject);
//				if (health != null) {
//					Debug.Log ("DAMAGE");
//					Debug.Log (whoFiredMe);
//					Debug.Log (hit.name);
//					if (bulletType == 1) {
//						if (collision.collider.tag == "Player") {
//							health.TakeDamage (25, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//						} else {
//							if (collision.collider.tag == "RoverPlayer") {
//								health.TakeDamage (20, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//							} else {
//								health.TakeDamage (32, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//							}
//						}
//					} else {
//						if (collision.collider.tag == "Player") {
//							health.TakeDamage (30, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//						} else {
//							if (collision.collider.tag == "RoverPlayer") {
//								health.TakeDamage (24, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//							} else {
//								health.TakeDamage (38, collision.gameObject.GetComponent<Player_ID> ().userNameLocal, whoFiredMe);
//							}
//						}
//					}
//				}
//			} 
//			}
//		//}
//	} */
   
//    public void killBulletOwl(){
//		bool flipOwl = false;
//		if (gameObject.GetComponent<SpriteRenderer> () != null) {
//			flipOwl = gameObject.GetComponent<SpriteRenderer> ().flipX;
//		}
//        if (!isServer)
//        {
//            CmdKillOwl(gameObject.transform.position);
//        }
//        else
//        {
//           ServerKillOwl(gameObject.transform.position);
//        }


//        if (!isServer)
//        {
//            Destroy(gameObject);
//        }
//    }

//	public void killBullet(){
//		if (isOwl) {
//            if (!isServer)
//            {
//                CmdKillOwl(gameObject.transform.position);
//            }
//            else
//            {
//               ServerKillOwl(gameObject.transform.position);
//            }
//        }
//        if(bulletType == 3)
//        {
           
//           if(isServer && hasSpawnedFire == false) {
//                hasSpawnedFire = true;
//                SpawnFireballExplosion(gameObject.transform.position, whoFiredMe, objectWhoFiredMe);

//            }
//        }
//        canDamageRock = false;
//        //NetworkServer.Destroy(gameObject);
//        if (isServer)
//        {
//            Destroy(gameObject);
//        }

//    }

//	[Command]
//	void CmdKillOwl(Vector2 pos){
//		var bullet = (GameObject)Instantiate (
//			             owlFeathers,
//			             pos,
//			             owlFeathers.transform.rotation);

//		NetworkServer.Spawn (bullet);

//	//	RpcAddForceOnOwl(bullet, flipped);
//	}

//    void SpawnFireballExplosion(Vector2 pos, string WFM, GameObject objWFM)
//    {
//        Debug.Log("TRUE NOW");
//        var explos = (GameObject)Instantiate(
//                        fireballExp,
//                        pos,
//                        fireballExp.transform.rotation);
//        NetworkServer.Spawn(explos);
//        explos.GetComponent<Bullet>().whoFiredMe = WFM;
//        explos.GetComponent<Bullet>().objectWhoFiredMe = objWFM;

//    }

//    [Command]
//    void CmdSpawnFireballExplosion(Vector2 pos, string WFM, GameObject objWFM)
//    {
//        Debug.Log("TRUE NOW");
//        var explos = (GameObject)Instantiate(
//                      fireballExp,
//                      pos,
//                      fireballExp.transform.rotation);
//        NetworkServer.Spawn(explos);
//        explos.GetComponent<Bullet>().whoFiredMe = WFM;
//        explos.GetComponent<Bullet>().objectWhoFiredMe = objWFM;
//    }
//    void ServerKillOwl(Vector2 pos)
//    {
//        if (!hasSpawnedFire)
//        {
//            var bullet = (GameObject)Instantiate(
//                             owlFeathers,
//                             pos,
//                             owlFeathers.transform.rotation);

//            NetworkServer.Spawn(bullet);
//            hasSpawnedFire = true;
//        }
//        //  RpcAddForceOnOwl(bullet, flipped);
//    }

//    public int collisionWithAmt(float amtOfDamage, string playerTag)
//    {
//        float amtToReturn = 0;
//        if (playerTag == "Player")
//        {
//            amtToReturn = amtOfDamage;
//            // health.TakeDamage(25, collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//        }
//        else if (playerTag == "RoverPlayer")
//        {
//            amtToReturn = (amtOfDamage / 125) * 100;
//            //  health.TakeDamage(20, collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//        }
//        else if (playerTag == "VenusianPlayer")
//        {
//            amtToReturn = (amtOfDamage / 80) * 100;
//            //health.TakeDamage(32, collision.gameObject.GetComponent<Player_ID>().userNameLocal, whoFiredMe);
//        }else if(playerTag == "JuppernautPlayer")
//        {
//            amtToReturn = (amtOfDamage / 200) * 100;
//        }
//        return (int)amtToReturn;
//    }
//    /*[ClientRpc]
//	void RpcAddForceOnOwl(GameObject bullet2,  bool flipped){
//		if (flipped) {
//			bullet2.GetComponent<SpriteRenderer> ().flipX = true;
//		}

//	}*/
//    public void grenadeExploded()
//    {
//        if (!isServer)
//        {
//            return;
//        }
//        else
//        {
//            SpawnFireballExplosion(gameObject.transform.position, whoFiredMe, objectWhoFiredMe);
//        }
//    }

//}
//*/