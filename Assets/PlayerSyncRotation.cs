using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSyncRotation : NetworkBehaviour {

	[SyncVar] bool syncFlipped = false;
	[SyncVar] float syncZRot = 0;
//	[SyncVar] float syncWRot = 0;

	public Animator playerAnim;
	public GameObject arm;
	public bool hasBird = false;

	[SerializeField] private SpriteRenderer playerRenderer;
	[SerializeField] private SpriteRenderer birdRenderer;
	[SerializeField] private Animator birdAnimator;
	[SerializeField] private SpriteRenderer armRenderer;
	[SerializeField] private Transform armTransform;
//	[SerializeField] float lerpRate = 15;

	[SerializeField] GameObject mapIcon;

    private float threshold = 0.1f;
    private bool lastValue = false;
	private float lastRotation;
	private bool lastSyncFlipped = false;

	private bool lastDead = false;
	private bool lastDeadMap = false;
	private bool lastArmActive = false;

	public bool isRover = false;
    public bool isJupper = false;
    public bool isVenusian = false;

	private bool drag = false;

	private bool roverCanShoot = true;
	[SerializeField] Sprite roverGunRed;
	[SerializeField] Sprite roverGunGreen;

    [SerializeField] GameObject roverFireParticles;

    private float previousZRot;
    private float previousTimeRot;
    private float timeForLastUpdate;
    private float timeSinceLastUpdateArr;
    private float pastSyncZ;
    private float timeExpired;
    Quaternion armZRotation;
    Quaternion pArmZRotation;
    private PlayerSyncPosition playerSyncPosition;

    void Start(){
        playerSyncPosition = transform.gameObject.GetComponent<PlayerSyncPosition>();
        if (!isLocalPlayer && isRover || isVenusian) {
			arm.SetActive (true);
		}
	}
	void FixedUpdate(){
		TransmitRotations ();
        UpdateArm();
	}
	void UpdateArm(){
		if (!isLocalPlayer) {
			playerRenderer.flipX = syncFlipped;
			if (hasBird) {
				birdRenderer.flipX = syncFlipped;
				birdAnimator.SetBool ("right", syncFlipped);
			}
			if (syncFlipped != lastSyncFlipped) {
				
				lastSyncFlipped = syncFlipped;
              
                if (syncFlipped) {
					
					Vector3 pos = armTransform.localPosition;
					pos.x = -0.31f; 
					armTransform.localPosition = pos;
					armRenderer.flipY = true;
                  
                } else {
					
					Vector3 pos = armTransform.localPosition;
					pos.x = 0.31f; 
					armTransform.localPosition = pos;
					armRenderer.flipY = false;
                   
                }
                if (isRover)
                {
                    Quaternion stupidTemp = roverFireParticles.transform.rotation;
                    Vector3 stupiderTemp = roverFireParticles.transform.localPosition;
                    if (syncFlipped)
                    {
                        stupidTemp.y = 180;
                        stupiderTemp.x = 0.57f;
                    }
                    else
                    {
                        stupiderTemp.x = -0.57f;
                        stupidTemp.y = 0;
                    }
                    roverFireParticles.transform.localPosition = stupiderTemp;
                    roverFireParticles.transform.rotation = stupidTemp;
                }
            }
           
           
          

            if (hasBird) {
				birdAnimator.SetBool ("dance", playerAnim.GetBool ("dance"));
				birdAnimator.SetBool ("hidden", playerAnim.GetBool ("birdFired"));
			} 
			if (isRover) {
				if(roverCanShoot != playerAnim.GetBool("canShoot")){
					roverCanShoot = playerAnim.GetBool ("canShoot");
					if (roverCanShoot) {
						armRenderer.sprite = roverGunGreen;
					} else {
						armRenderer.sprite = roverGunRed;
					}
				}
			}
			if (playerAnim.GetBool ("dead") != lastDeadMap) {
				lastDeadMap = playerAnim.GetBool ("dead");
				if (lastDeadMap) {
					mapIcon.SetActive (false);
					if (isRover || isVenusian) {
						arm.SetActive (false);
					}
				} else {
					mapIcon.SetActive (true);
					if (isRover || isVenusian) {
						arm.SetActive (true);
					}
				}
			}
			if (playerAnim.GetBool ("dead") != lastDead || !isRover && !isJupper && playerAnim.GetBool("shooting") != lastArmActive) {
				if(!isRover && !isJupper) lastArmActive = playerAnim.GetBool ("shooting");
				lastDead = playerAnim.GetBool ("dead");
				if (!isRover && !isVenusian) {
					if (lastArmActive && !lastDead) {
						arm.SetActive (true);
					} else {
						arm.SetActive (false);
					}
				}
			}

           // LerpRotation();
        }
		}
    void LerpRotation()
    {
        if (isServer)
        {
            return;
        }
        if (Mathf.Abs(previousZRot - syncZRot) > 0.01)
        {
            previousZRot = syncZRot;
            timeForLastUpdate = playerSyncPosition.GetAverageUpdateTimes() * 2;
            timeSinceLastUpdateArr = 0;
            pastSyncZ = armTransform.localEulerAngles.z;
            timeExpired = 0;
            armZRotation =  Quaternion.Euler(0, 0, syncZRot);
            pArmZRotation = Quaternion.Euler(0, 0, pastSyncZ);
        }
        if (timeSinceLastUpdateArr < 1)
        {
            timeExpired += Time.deltaTime * 1000;
            timeSinceLastUpdateArr = (timeExpired) / timeForLastUpdate;
        }
        if (timeSinceLastUpdateArr > 1)
        {
            timeSinceLastUpdateArr = 1;
        }

        armTransform.localRotation = armTransform.localRotation = Quaternion.Lerp(pArmZRotation, armZRotation, timeSinceLastUpdateArr);
    }
    [Command]
	void CmdProvideRotationsToServer(bool flip){
		syncFlipped = flip;
	}
	[Command]
	void CmdProvideRotationsToServerArm(float zPos){
		
		syncZRot = zPos;
	}
	[ClientCallback]
	void TransmitRotations(){
		if (isLocalPlayer && playerRenderer.flipX != lastValue) {
			CmdProvideRotationsToServer (playerRenderer.flipX);
			lastValue = playerRenderer.flipX;
		}
		/*if (isLocalPlayer && arm.activeSelf && Time.time - previousTimeRot > 0.1 && Mathf.Abs(armTransform.localEulerAngles.z - lastRotation) > threshold) {
            previousTimeRot = Time.time;
            CmdProvideRotationsToServerArm (armTransform.localEulerAngles.z);
			lastRotation = armTransform.localEulerAngles.z;
		}*/
	}
		
}
