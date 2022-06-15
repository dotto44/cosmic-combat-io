using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using Mirror;

public class PlayerController : PhysicsObject
{
    //test
    [SerializeField] GameObject plasmaPopper;
    [SerializeField] GameObject heartPopper;
    private bool fallenOff = false;
    private int collectedStars = 0;
    private int basicAttacks;
    private int specialAttacks;

    float startDancingTime = 0;
    [SerializeField] PlayerReferences playerReferences;

    public GameObject normanPlasmaParticles;
    private bool normBurst = false;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float flightSpeed = 50.0f;
    private float sprintMultiplier = 1;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool canPutGunAway = true;
    private bool isFlying = false;
    private bool isSmoking = false;
    private bool isBooting = false;

    private CinemachineVirtualCamera followCamera;

    [SerializeField] JupArm jupArm;
    [SerializeField] HealthBars healthBars;
    public float amtFuel = 264;
    private float lastAmtFuel = 264;

   

    private bool shooting = false;
    public GameObject arm;
    public Animator armAnim;

    private SpriteRenderer armRenderer;
    private bool armLeft = false;

    bool isInFlight;

    public GameObject bulletPrefab;
    [SerializeField] GameObject localBulletPrefab;
    [SerializeField] GameObject localOwlPrefab;
    [SerializeField] GameObject localDragonPrefab;
    public GameObject arrowPrefab;
    public GameObject fireballPrefab;
    public GameObject grenadePrefab;
    public GameObject fireballGravPrefab;
    public GameObject localFireballGravPrefab;
    public GameObject owlPrefab;
    public GameObject dragonPrefab;
    public GameObject roverBulletPrefab;
    public Transform bulletSpawn;
    public Transform bulletSpawn2;
    public Rigidbody2D bulletRigid;

    public GameObject HUD;
    float shotTime = 10;
    private const float normShotMax = 20;
    private float shotTimeMax = normShotMax;
    Vector3 lastValidPos = new Vector3(0, 0, 0);
    Vector3 lastValidPos2 = new Vector3(0, 0, 0);

    private bool lastFlip = false;
    bool flipSprite = false;
    private Health healthScript;

    private bool isLocked = false;

    [SerializeField] Sprite roverGunRed;
    [SerializeField] Sprite roverGunGreen;
    private SpriteRenderer roverGun;
    private bool roverCanShoot = true;
    private bool roverCouldShoot = true;

    private bool dance = false;
    private bool focus = false;

    private float specialMult = 1;
    private bool special = false;

    private bool dead = false;

    private bool hasAchieved = false;
    private bool pressingLeft = false;
    private bool pressingRight = false;


    double horAxis = 0;


    //[SerializeField] Animatio roverAnim;
    [SerializeField] SpriteRenderer mapIdentifier;

    [SerializeField] GameObject focusBarHolder;
    [SerializeField] Animator focusBar;
    private bool previousFocus = false;

    [SerializeField] SpriteRenderer birdRenderer;
    [SerializeField] Animator birdAnimator;
    [SerializeField] GameObject owl;

    AudioSource audioData;

    private bool owlAttackUsed = false;

    Vector3 aPosition1;

    [SerializeField] GameObject roverFireParticles;
    ParticleSystem roverFireParticleSystem;
    public float armPosMove = 0.31f;
    [SyncVar(hook = nameof(UpdateFireballSprite))]
    private int currentReloadStage = 0;
    private NetworkStartPosition[] spawnPoints;

    bool canFly = true;

    [SerializeField] GameObject arm2;
    SpriteRenderer armRenderer2;

    bool flipSpriteRover = false;
    bool canFlipRover = false;

    [SerializeField] GameObject goldParticles;

    private bool left;
    private bool right;
   

    private float normArmY = -0.11f;
    private float jupPoundTime = 0;

    private bool inputLock;
    int[] boundaries;

    Vector3 pivotPoint;

    private float groundMovementSpeed = 0.0f;
    private float airMovementSpeed = 0.0f;
    public void setUpRover()
    {
        arm.SetActive(true);
        roverGun = arm.GetComponent<SpriteRenderer>();
        shotTimeMax = 70;
        roverFireParticleSystem = roverFireParticles.GetComponent<ParticleSystem>();
    }
    public void setUpNep()
    {
        shotTimeMax = 20;
    }
    public void setUpJup()
    {
        arm.SetActive(true);
        roverGun = arm.GetComponent<SpriteRenderer>();
        shotTimeMax = 85;
        armPosMove = 0.4f;
        armRenderer2 = arm2.GetComponent<SpriteRenderer>();
    }
    public void setUpVen()
    {
        shotTimeMax = 40;
        armPosMove = 0.54f;
        arm.SetActive(true);
    }
    public void setUpPluto()
    {
        shotTimeMax = 40;
        armPosMove = 0.54f;
        arm.SetActive(true);
    }
    public void setUpNorm()
    {
        arm.SetActive(true);
    }
    public void setUpChars()
    {
        if (StatsHolder.characterSelected == 1)
        {
            setUpRover();
        }
        if (StatsHolder.characterSelected == 3)
        {
            setUpJup();
        }
        if (StatsHolder.characterSelected == 4)
        {
            setUpNep();
        }
        if (StatsHolder.characterSelected == 2)
        {
            setUpVen();
        }
        if (StatsHolder.characterSelected == 5)
        {
            setUpPluto();
        }
        if(StatsHolder.characterSelected == 0)
        {
            setUpNorm();
        }
    }
    public void setUpUniversalObjectComponents()
    {
        HUD.SetActive(true);
        armRenderer = arm.GetComponent<SpriteRenderer>();
        healthScript = GetComponent<Health>();
        healthBars.setFuelMeter(amtFuel);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        followCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
        followCamera.Follow = gameObject.transform;
        animator.SetInteger("costume", StatsHolder.currentSelectedSkin);
        pivotPoint = new Vector3(transform.position.x, arm.transform.position.y);
    }
    public override void OnStartLocalPlayer()
    {
        
        mapIdentifier.color = new Color(82/255f, 1, 33/255f, 1);

        setUpChars(); //IF CHARACTER IS NOT NORM, SET THEIR DEFAULT STATS

        setBoundaries(); //GRABS BOUNDARIES FROM MAP INFO ON SPAWN POINTS

        setUpUniversalObjectComponents(); //GRABS COMPONENTS/BASIC ANIMATION STATS FOR ANY CHAR

    }
    public void setBoundaries()
    {
        boundaries = GameObject.FindWithTag("SpawnPointManager").GetComponent<MapInfo>().getBoundaries();
    }
    public void checkNormArmY()
    {
        if(StatsHolder.characterSelected == 0 && shooting)
        {
          
        }
    }

    public float setNormArmY()
    {
        return -0.11f;
    }

    public void updateJupsGun()
    {
        if (shotTime < 68 && shotTime > 51 && currentReloadStage == 5)
        {
            roverGun.sprite = jupArm.getArmSprite(1);
            CmdUpdateFireballSprite(4);
            currentReloadStage = 4;
        }
        if (shotTime < 51 && shotTime > 34 && currentReloadStage == 4)
        {
            roverGun.sprite = jupArm.getArmSprite(2);
            CmdUpdateFireballSprite(3);
            currentReloadStage = 3;
        }
        if (shotTime < 34 && shotTime > 17 && currentReloadStage == 3)
        {
            roverGun.sprite = jupArm.getArmSprite(3);
            CmdUpdateFireballSprite(2);
            currentReloadStage = 2;
        }
        if (shotTime < 17 && shotTime > 0 && currentReloadStage == 2)
        {
            roverGun.sprite = jupArm.getArmSprite(4);
            CmdUpdateFireballSprite(1);
            currentReloadStage = 1;
        }
        if (shotTime <= 0 && currentReloadStage == 1)
        {
            roverGun.sprite = jupArm.getArmSprite(5);
            CmdUpdateFireballSprite(0);
            currentReloadStage = 0;
        }
    }

    public void lockCharacterIfDead()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade") || animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade 0") || animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade 0 0") || animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade 0 0 0"))
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
    }

    public void normArmVariation()
    {

    }

    public void checkForFallingDeath()
    {
        if (gameObject.transform.position.y < boundaries[2] && !fallenOff || gameObject.transform.position.y > boundaries[3] && !fallenOff || gameObject.transform.position.x > boundaries[1] && !fallenOff || gameObject.transform.position.x < boundaries[0] && !fallenOff)
        {
            fallenOff = true;
            healthScript.fallOff();
            if (isLocalPlayer)
            {
                GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(0);
            }
        }
    }
    public void setGroundMovementSpeed(float groundMovementSpeed)
    {
        Debug.Log("Changed Ground Speed to: " + groundMovementSpeed);
        if(groundMovementSpeed < 0.01)
        {
            airMovementSpeed = this.groundMovementSpeed;
        }
        this.groundMovementSpeed = groundMovementSpeed;
    }
    public double smoothedHorizontalMovement(float defaultValue)
    {
        airMovementSpeed = 0;
        if (!inputLock)
        {
            right = GameInputManager.GetKey("Right");
            left = GameInputManager.GetKey("Left");
        }
        else
        {
            right = false;
            left = false;
        }
       

        if (left && right)
        {
            horAxis = defaultValue;
        }
        else if (left || right)
        {
            if (left && System.Math.Abs(horAxis + 1 - defaultValue) > 0.01 )
            {

                if (horAxis > 0.01 + defaultValue)
                {

                    horAxis = defaultValue;
                }
                else
                {
                    horAxis -= Time.deltaTime * 5;
                }
            }
            if (right && System.Math.Abs(horAxis - 1 - defaultValue) > 0.01)
            {

                if (horAxis < -0.01 + defaultValue)
                {
                    horAxis = defaultValue;
                }
                else
                {
                    horAxis += Time.deltaTime * 5;
                }
            }
        }
        else
        {
            horAxis = defaultValue;
        }
        if (horAxis > 1 + defaultValue)
        {
            horAxis = 1 + defaultValue;
        }
        if (horAxis < -1 + defaultValue)
        {
            horAxis = -1 + defaultValue;
        }
     
        return horAxis;
    }
    public double smoothedHorizontalIceMovement()
    {
        if (!inputLock)
        {
            right = GameInputManager.GetKey("Right");
            left = GameInputManager.GetKey("Left");
        }
        else
        {
            right = false;
            left = false;
        }

        if(Mathf.Abs(velocity.x) < 0.01)
        {
            horAxis = 0;
        }
        if (left && right)
        {
            //horAxis = 0;
        }
        else if (left || right)
        {
            if (left && System.Math.Abs(horAxis + 1) > 0.01)
            {

                if (horAxis > 0.01)
                {

                    horAxis -= Time.deltaTime * 1;
                }
                else
                {
                    horAxis -= Time.deltaTime * 1;
                }
            }
            if (right && System.Math.Abs(horAxis - 1) > 0.01)
            {

                if (horAxis < -0.01)
                {
                    horAxis += Time.deltaTime * 1;
                }
                else
                {
                    horAxis += Time.deltaTime * 1;
                }
            }
        }
        else
        {
            if (horAxis < -0.05)
            {
                horAxis += Time.deltaTime/1.5;
                if (horAxis > 0)
                    horAxis = 0;
            } else if (horAxis > 0.05)
            {
                horAxis -= Time.deltaTime/1.5;
                if (horAxis < 0)
                    horAxis = 0;
            }
            else
            {
                horAxis = 0;
            }
        }
        if (horAxis > 1)
        {
            horAxis = 1;
        }
        if (horAxis < -1)
        {
            horAxis = -1;
        }
        //  horAxis = Input.GetAxis("Horizontal");
        
        return horAxis;
    }
    public double smoothedHorizontalMovementWithMomentum()
    {
       /* if(groundMovementSpeed < 0.01 && airMovementSpeed < 0.01)
        {
            airMovementSpeed = groundMovementSpeed;
        }*/
       
        if (!inputLock)
        {
            right = GameInputManager.GetKey("Right");
            left = GameInputManager.GetKey("Left");
        }
        else
        {
            right = false;
            left = false;
        }

       /* if (Mathf.Abs(velocity.x) < 0.01)
        {
            horAxis = 0;
        }*/
        if (left && right)
        {
            //horAxis = 0;
        }
        else if (left || right)
        {
            if (left && System.Math.Abs(horAxis + 1 - airMovementSpeed) > 0.01)
            {
                horAxis -= Time.deltaTime * 5;
            }
            if (right && System.Math.Abs(horAxis - 1 - airMovementSpeed) > 0.01)
            {
                horAxis += Time.deltaTime * 5;
            }
        }
        else
        {
            if (horAxis < -0.05 + airMovementSpeed)
            {
                horAxis += Time.deltaTime * 5;
                if (horAxis > airMovementSpeed)
                    horAxis = airMovementSpeed;
            }
            else if (horAxis > 0.05 + airMovementSpeed)
            {
                horAxis -= Time.deltaTime * 5;
                if (horAxis < airMovementSpeed)
                    horAxis = airMovementSpeed;
            }
            else
            {
                horAxis = airMovementSpeed;
            }
        }
        if (horAxis > 1 + airMovementSpeed)
        {
            horAxis = 1 + airMovementSpeed;
        }
        if (horAxis < -1 + airMovementSpeed)
        {
            horAxis = -1 + airMovementSpeed;
        }
        //  horAxis = Input.GetAxis("Horizontal");
        airMovementSpeed *= 0.95f;
        return horAxis;
    }
    public double smoothedHorizontalMovementWithMomentumQuick()
    {
        if (!inputLock)
        {
            right = GameInputManager.GetKey("Right");
            left = GameInputManager.GetKey("Left");
        }
        else
        {
            right = false;
            left = false;
        }

        if (Mathf.Abs(velocity.x) < 0.01)
        {
            horAxis = 0;
        }
        if (left && right)
        {
            //horAxis = 0;
        }
        else if (left || right)
        {
            if (left && System.Math.Abs(horAxis + 1) > 0.01)
            {

                if (horAxis > 0.01)
                {

                    horAxis = Time.deltaTime * 10;
                }
                else
                {
                    horAxis -= Time.deltaTime * 5;
                }
            }
            if (right && System.Math.Abs(horAxis - 1) > 0.01)
            {

                if (horAxis < -0.01)
                {
                    horAxis += Time.deltaTime * 10;
                }
                else
                {
                    horAxis += Time.deltaTime * 5;
                }
            }
        }
        else
        {
            if (horAxis < -0.05)
            {
                horAxis += Time.deltaTime * 5;
                if (horAxis > 0)
                    horAxis = 0;
            }
            else if (horAxis > 0.05)
            {
                horAxis -= Time.deltaTime * 5;
                if (horAxis < 0)
                    horAxis = 0;
            }
            else
            {
                horAxis = 0;
            }
        }
        if (horAxis > 1)
        {
            horAxis = 1;
        }
        if (horAxis < -1)
        {
            horAxis = -1;
        }
        //  horAxis = Input.GetAxis("Horizontal");
        return horAxis;
    }
    public void flipSpriteBasedOnMousePos(float lookPosX)
    {
        if (lookPosX > gameObject.transform.position.x)
        {
            flipSprite = false;
        }
        if (lookPosX <= gameObject.transform.position.x)
        {
            flipSprite = true;
        }
    }

    public void normalFireFunction()
    {
        if (GameInputManager.GetKeyDown("Fire1") && !inputLock)
        {
            shooting = true;
        }
    }
    public void normFireFunction()
    {
        if (GameInputManager.GetKeyDown("Fire1") && !inputLock || normBurst && !shooting)
        {
            if (!shooting && !dead)
            {
                arm.SetActive(true);
                canPutGunAway = false;
            }
            shooting = true;

        }
    }
    public void fireFunctionPicker()
    {
        if (StatsHolder.characterSelected == 0)
        {
            normFireFunction();
        }
        else
        {
            normalFireFunction();
        }
    }

    public void executeSpriteFlipBasedOnCharacter()
    {
        if(StatsHolder.characterSelected == 1)
        {
            executeSpriteFlipRover();
        }
        else
        {
            executeSpriteFlipNormal();
        }
    }
    public void executeSpriteFlipNormal()
    {
        spriteRenderer.flipX = flipSprite;
        canFlipRover = false;
    }
    public void executeSpriteFlipRover() 
    {
        if (!special || Mathf.Abs(velocity.x) < 0.001)
        {
            executeSpriteFlipNormal();
        }
        else
        {

            canFlipRover = true;
            if (velocity.x > 0)
            {
                spriteRenderer.flipX = false;
                flipSpriteRover = false;
            }
            else
            {
                spriteRenderer.flipX = true;
                flipSpriteRover = true;
            }
            Quaternion stupidTemp = roverFireParticles.transform.rotation;
            Vector3 stupiderTemp = roverFireParticles.transform.localPosition;
            if (flipSpriteRover == true)
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

    public void updateVenBirdAndReload()
    {
        if (StatsHolder.characterSelected == 2)
        {
            shotTime -= Time.deltaTime * 30;
            birdRenderer.flipX = flipSprite;
            birdAnimator.SetBool("right", flipSprite);
        }
    }
    public void flipArmBasedOnCharacter()
    {
        if(StatsHolder.characterSelected == 0)
        {
            executeFlipNormArm();
        }
        else if(StatsHolder.characterSelected == 3)
        {
            executeFlipJupArm();
        }
        else
        {
            executeFlipNormalArm();
        }
    }

    public void executeFlipNormArm()
    {
        if (flipSprite != lastFlip)
        {
            lastFlip = flipSprite;
            if (flipSprite)
            {
                Vector3 pos = arm.transform.localPosition;
                pos.x = -(armPosMove);
                pos.y = setNormArmY();
                arm.transform.localPosition = pos;
                armRenderer.flipY = true;
                armLeft = true;
            }
            else
            {

                Vector3 pos = arm.transform.localPosition;
                pos.x = armPosMove;
                pos.y = setNormArmY();
                arm.transform.localPosition = pos;
                armRenderer.flipY = false;
                armLeft = false;
            }
        }
    }

    public void executeFlipJupArm()
    {
        if (flipSprite != lastFlip)
        {
            lastFlip = flipSprite;
            if (flipSprite)
            {
                Vector3 pos = arm.transform.localPosition;
                pos.x = -(armPosMove);
                arm.transform.localPosition = pos;
                armRenderer.flipY = true;
                armLeft = true;
                    Vector3 pos2 = arm2.transform.localPosition;
                    pos2.x = 0.87f;
                    arm2.transform.localPosition = pos2;
                    armRenderer2.flipX = true;
            }
            else
            {

                Vector3 pos = arm.transform.localPosition;
                pos.x = armPosMove;
                arm.transform.localPosition = pos;
                armRenderer.flipY = false;
                armLeft = false;
                    Vector3 pos2 = arm2.transform.localPosition;
                    pos2.x = -0.87f;
                    arm2.transform.localPosition = pos2;
                    armRenderer2.flipX = false;
            }
        }
    }
    public void executeFlipNormalArm()
    {
        if (flipSprite != lastFlip)
        {
            lastFlip = flipSprite;
            if (flipSprite)
            {
                Vector3 pos = arm.transform.localPosition;
                pos.x = -(armPosMove);
                arm.transform.localPosition = pos;
                armRenderer.flipY = true;
                armLeft = true;
            }
            else
            {
                Vector3 pos = arm.transform.localPosition;
                pos.x = armPosMove;
                arm.transform.localPosition = pos;
                armRenderer.flipY = false;
                armLeft = false;
            }
        }
    }
    public void calculateAngle(Vector3 lookPos)
    {
        if (armLeft == false && lookPos.x > gameObject.transform.position.x || armLeft == true && lookPos.x <= gameObject.transform.position.x)
        {
            pivotPoint.x = gameObject.transform.position.x;
            pivotPoint.y = arm.transform.position.y;
            lookPos = lookPos - pivotPoint;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           // Debug.Log("FIRST ANGLE: " + angle);
          // Debug.Log("ARM LEFT: " + armLeft);
           // Debug.Log("LOOK POS: " + lookPos.x);
           // Debug.Log("POS X: " + gameObject.transform.position.x);
    }
        else
        {
           
           // float angle = Mathf.Atan2(lastValidPos.y, lastValidPos.x) * Mathf.Rad2Deg;
           // arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           //Debug.Log("SECOND ANGLE: " + angle);
           // Debug.Log("ARM LEFT: " + armLeft);
            //Debug.Log("LOOK POS: " + lookPos.x);
            //Debug.Log("POS X: " + gameObject.transform.position.x);
        }
    }
    public void roverGunSpriteSwapLogic()
    {
        if (StatsHolder.characterSelected == 1 && roverCanShoot != roverCouldShoot)
        {
            roverCouldShoot = roverCanShoot;
            if (roverCanShoot == true)
            {
                roverGun.sprite = roverGunGreen;
            }
            else
            {
                roverGun.sprite = roverGunRed;
            }
        }
    }
    public void reloadAnimationLogic()
    {
        if (StatsHolder.characterSelected == 0)
        {
            reloadAnimationLogicNorm();
        }
        else if (StatsHolder.characterSelected == 1)
        {
            reloadAnimationLogicRover();
        }
        else if (StatsHolder.characterSelected == 2)
        {
            updateVenBirdAndReload();
        }
        else
        {
            reloadAnimationLogicNormal();
        }
    }
        public void reloadAnimationLogicNorm()
    {
        if (shotTime >= 0)
        {
            shotTime -= Time.deltaTime * 30;
            //animator.SetBool("canShoot", false);

        }
    }
    public void reloadAnimationLogicRover()
    {
        if (shotTime >= 0)
        {
            shotTime -= Time.deltaTime * 30;
            roverCanShoot = false;
            animator.SetBool("canShoot", false);
        }
        else
        {
            roverCanShoot = true;
            animator.SetBool("canShoot", true);
        }
    }
    public void reloadAnimationLogicNormal()
    {
        if (shotTime >= 0)
        {
            shotTime -= Time.deltaTime * 30;
            animator.SetBool("canShoot", false);
        }
        else
        {
            animator.SetBool("canShoot", true);
        }
    }
    public void executeFireWavesAttackSmall()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdFireLittleWaves(gameObject.transform.position, GetName.userName);
    }
    public void executeFireWavesAttackBig()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdFireBigWaves(gameObject.transform.position, GetName.userName);
    }
    public void executeMainAttack(Vector3 dir, Vector3 shootPosition)
    {
        if (StatsHolder.characterSelected == 0)
        {
            canPutGunAway = true;
            if (!GameInputManager.GetKey("Fire1") && shooting && !normBurst)
            {
                shooting = false;
            }
          
            if (!normBurst)
            {
                usedBasicAttack();
            }
            CmdFire(dir, shootPosition, GetName.userName, special, arm.transform.localEulerAngles.z);
        }
        else  if (StatsHolder.characterSelected == 2)
            {
                canPutGunAway = true;
                if (!GameInputManager.GetKey("Fire1") && shooting)
                {
                    shooting = false;

                }
                usedBasicAttack();
                CmdFireArrow1(dir, shootPosition, GetName.userName, arm.transform.rotation, arm.transform.localEulerAngles.z);

            }
            else if (StatsHolder.characterSelected == 1)
                {
                    usedBasicAttack();
                    CmdFireShotty(dir, shootPosition, GetName.userName, arm.transform.localEulerAngles.z);
                }
        else if(StatsHolder.characterSelected == 3)
                {
                    usedBasicAttack();
                    CmdFireball(dir, shootPosition, GetName.userName, arm.transform.localEulerAngles.z);
                    roverGun.sprite = jupArm.getArmSprite(0);
                    CmdUpdateFireballSprite(5);
                    currentReloadStage = 5;
        }
        else if (StatsHolder.characterSelected == 4)
        {
            usedBasicAttack();
            CmdNepSlash(transform.position, GetName.userName, flipSprite);
         
        }else if(StatsHolder.characterSelected == 5)
        {
            usedBasicAttack();
            plutoPunch(dir, shootPosition, GetName.userName);
        }

    }
    public void stopNepSwipe()
    {
        if (isLocalPlayer)
        {
            animator.SetBool("attack", false);
        }
    }
    public void shootingLogic()
    {
        if (shooting && !inputLock)
        {
            if (shotTime < 0)
            {
                shotTime = shotTimeMax;

                Vector3 sp = Camera.main.WorldToScreenPoint(pivotPoint);
                Vector3 dir = (lastValidPos - sp).normalized;
                Vector3 shootPosition = bulletSpawn.transform.position;

                if (flipSprite)
                {
                    shootPosition = bulletSpawn2.position;
                }
                executeMainAttack(dir, shootPosition);

            }
        }
    }

    public void allowFlight()
    {
        if (StatsHolder.characterSelected == 1 && special)
        {
            canFly = false;
        }
        else
        {
            canFly = true;
        }
    }

    public void handleJump()
    {
        if (GameInputManager.GetKeyDown("Jump") && grounded && !inputLock)
        {
            velocity.y = jumpTakeOffSpeed;
            groundNormal = new Vector2(0, 1);
            isOnIce = false;
        }
        else if (GameInputManager.GetKeyUp("Jump"))
        {

            if (velocity.y > 0 && !isFlying && !isBooting)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }
    }
    public void handleFlight()
    {
        if (GameInputManager.GetKeyDown("Jump") && !grounded && !inputLock || GameInputManager.GetKey("Jump") && !grounded && isFlying && !inputLock || GameInputManager.GetKey("Jump") && !grounded && isSmoking && !inputLock)
        {
            if (amtFuel > 0)
            {
                if (canFly)
                {
                    isFlying = true;
                    isOnIce = false;
                    isSmoking = false;
                }

            }
            else
            {
                if (canFly)
                {
                    isSmoking = true;
                    isFlying = false;
                }
            }
        }
    }

    public void duringAndEndFlightAndJump()
    {
        if (GameInputManager.GetKeyUp("Jump"))
        {
            isFlying = false;
            isSmoking = false;
        }
        if (isFlying || isBooting)
        {

            amtFuel -= Time.deltaTime * 20;

            if (velocity.y < 8)
            {
                velocity.y += (float)flightSpeed * Time.deltaTime;
            }

        }
        if (amtFuel != lastAmtFuel)
        {
            lastAmtFuel = amtFuel;
            healthBars.setFuelMeter(amtFuel);
        }

        if (GameInputManager.GetKey("Focus") && amtFuel < 264 && !inputLock)
        {
            focus = true;

            dance = false;

        }
    }
    public void roverSpecialLogic()
    {
        if (StatsHolder.characterSelected == 1)
        {
            special = true;
            usedSpecialAttack();
            specialMult = 1.7f;
        }
    }
    public void normSpecialLogic()
    {
        if (StatsHolder.characterSelected == 0)
        {
            if (amtFuel > 99)
            {
                if (!special)
                {
                    usedSpecialAttack();
                    amtFuel -= 100;
                    special = true;
                    shotTimeMax = normShotMax / 2;
                    normBurst = true;
                    StartCoroutine(StopBursting());
                    CmdAddPlasmaParticles();
                }
            }
        }
    }
    public void jupSpecialLogic()
    {
        if (StatsHolder.characterSelected == 3)
        {
            if (amtFuel > 87)
            {
                if (!special  && !grounded)
                {
                    jupPoundTime = 0;
                    usedSpecialAttack();
                    amtFuel -= 88;
                    special = true;
                }
            }
            else
            {
                special = false;
            }

        }
    }

    public void nepSpecialLogic()
    {
        if (StatsHolder.characterSelected == 4)
        {
            if (amtFuel > 70)
            {
                if (!special && !animator.GetCurrentAnimatorStateInfo(0).IsName("NeptunianSuper"))
                {
                    usedSpecialAttack();
                    amtFuel -= 71;
                    special = true;
                    focus = false;
                    dance = false;
                    animator.SetBool("bubble", true);
                }
            }

        }
    }




    public void venSpecialLogic()
    {
        if (StatsHolder.characterSelected == 2 && amtFuel > 49 && !owlAttackUsed)
        {
            usedSpecialAttack();
            amtFuel -= 50;
            owlAttackUsed = true;
            special = true;
            birdAnimator.SetBool("hidden", true);
            animator.SetBool("birdFired", true);
            Debug.Log("hide");
            special = false;
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = (lastValidPos - sp).normalized;
            Vector3 shootPosition = owl.transform.position;
            /*  if (flipSprite) {
                    shootPosition = bulletSpawn2.position;
                }*/
            StartCoroutine(unhideOwl());
            if (StatsHolder.currentSelectedSkin == 0)
            {
                CmdFireOwl(dir, shootPosition, GetName.userName, flipSprite);
            }
            else if (StatsHolder.currentSelectedSkin == 1)
            {
                CmdFireDragon(dir, shootPosition, GetName.userName, flipSprite);
            }
        }
    }
    public void specialLogic()
    {
        if (GameInputManager.GetKeyDown("Special") && !dead && amtFuel > 0 && !inputLock)
        {
            roverSpecialLogic();
            normSpecialLogic();
            jupSpecialLogic();
            venSpecialLogic();
            nepSpecialLogic();
        }
    }
    public void engageDance()
    {
        if (GameInputManager.GetKeyDown("Dance") && !focus && !inputLock)
        {
            dance = true;
            startDancingTime = Time.fixedTime;
        }
    }
    public void stopDance()
    {
        if (!grounded || Mathf.Abs((float)horAxis - groundMovementSpeed) > 0.01 || isFlying || isSmoking || shooting)
        {

            dance = false;
            focus = false;
        }
    }
    public void startStopFocus()
    {
        animator.SetBool("dance", dance);
    }
    public void putGunAway()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            if (canPutGunAway)
            {
                shooting = false;
            }
        }
    }
    public void checkForDancingAchievement()
    {
        if (dance && Time.fixedTime - startDancingTime > 25 && !hasAchieved)
        {
            hasAchieved = true;
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(11);
        }
    }

    public void setAnimatorBools()
    {
        generalBoolSetting();
    }
    public void generalBoolSetting()
    {
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", (Mathf.Abs((float)horAxis - groundMovementSpeed) > 0.05 ? Mathf.Abs((float)horAxis - groundMovementSpeed) : 0));
        //animator.SetFloat("velocityX", (Mathf.Abs(velocity.x) / maxSpeed) > 0.25 ? (Mathf.Abs(velocity.x) / maxSpeed) : 0);
        if (velocity.x > 0.01 && spriteRenderer.flipX || velocity.x < -0.01 && !spriteRenderer.flipX) {
            animator.SetFloat("directX", -1);
        }
        else
        {
            animator.SetFloat("directX", 1);
        }
        
        if (amtFuel < 264 && focus)
        {
            lastAmtFuel = amtFuel;
            animator.SetBool("focus", focus);
        }
        else
        {
            animator.SetBool("focus", false);
        }

        animator.SetBool("rocketing", isFlying);
        if (!grounded)
        {
            animator.SetBool("isSmoking", isSmoking);
        }
        else
        {
            animator.SetBool("isSmoking", false);
        }
        normBoolSetting();
        roverBoolSetting();
        venBoolSetting();
        nepBoolSetting();
    }
    public void normBoolSetting()
    {

    }
    public void roverBoolSetting()
    {
        if(StatsHolder.currentSelectedSkin == 2 && StatsHolder.currentSelectedSkin == 1)
        {
            animator.SetFloat("velocityY", velocity.y / maxSpeed);
        }
    }
    public void venBoolSetting()
    {
        if(StatsHolder.characterSelected != 2)
        {
            return;
        }
        birdAnimator.SetBool("dance", dance);
        animator.SetFloat("velocityY", velocity.y / maxSpeed);
    }
    public void jupBoolSetting()
    {
        if (StatsHolder.characterSelected != 3)
        {
            return;
        }
        animator.SetFloat("velocityY", velocity.y / maxSpeed);
    }
    public void nepBoolSetting()
    {
        if (StatsHolder.characterSelected != 4)
        {
            return;
        }
      // animator.SetFloat("velocityY", velocity.y / maxSpeed);
    }

    public void manageConstantUseSpecials()
    {
       
        if (amtFuel > 0 && special)
        {
            healthBars.setFuelMeter(amtFuel);
            lastAmtFuel = amtFuel;
        }
        manageRoverDash();
        manageJupSpecial();
        manageNormSpecial();
    }
    public void manageRoverDash()
    {
        if(StatsHolder.characterSelected != 1)
        {
            return;
        }
        if (amtFuel > 0 && special)
        {
                if (Mathf.Abs(velocity.x) > 0.001)
                {
                    amtFuel -= Time.deltaTime * 27;
                    animator.SetBool("rage", special);
                }
               
        }
        else
        {
                animator.SetBool("rage", false);
                
        }
    }
    public void endJupSpecial()
    {
        inputLock = false;
        special = false;
    }
    public void manageJupSpecial()
    {

        if (StatsHolder.characterSelected == 3)
        {
            if (!special)
            {
                animator.SetInteger("groundPound", 0);
            }
            else
            {
                inputLock = true;
                jupPoundTime += 1 * Time.deltaTime;
                if(jupPoundTime < 1)
                {
                    animator.SetInteger("groundPound", 1);
                }
                else if(jupPoundTime < 3)
                {
                    animator.SetInteger("groundPound", 2);
                }
                else
                {
                    animator.SetInteger("groundPound", 2);
                }


                velocity.y -= flightSpeed * Time.deltaTime;
               /* if (grounded)
                {
                    inputLock = false;
                    special = false;
                }*/
            }

        }
    }
    public void manageNormSpecial()
    {
        if (StatsHolder.characterSelected == 0)
        {
            animator.SetBool("shooting", shooting);
        }
    }
    protected override void ComputeVelocity()
    {

        if (StatsHolder.characterSelected == 3)
        {
            updateJupsGun(); //SENDS THE RELOAD STAGE TO SERVER 
        }

        lockCharacterIfDead(); //IF CHARACTER IS IN DEAD STATE, LOCK CONTROLS ETC

        flipArmBasedOnCharacter();


        if (!isLocked)
        {
            checkForFallingDeath(); //KILLS PLAYER IF OOB

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); //GET MOUSE POS
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);

            if (armLeft == false && lookPos.x > gameObject.transform.position.x || armLeft == true && lookPos.x < gameObject.transform.position.x)
            {
                lastValidPos = Input.mousePosition;
            }
          
            Vector2 move = Vector2.zero;

            if (isOnIce)
            {
                move.x = (float)smoothedHorizontalIceMovement();
            } else if (!grounded)
              {
                  move.x = (float)smoothedHorizontalMovementWithMomentum();
              }
              else
              {
                  move.x = (float)smoothedHorizontalMovement(groundMovementSpeed);
              }

            //move.x = (float)smoothedHorizontalMovementWithMomentum();
            //HORIZONTAL MOVEMENT WITH CALCED SMOOTHING

            // move.x = Input.GetAxis("Horizontal"); //UNITY MOVEMENT

            fireFunctionPicker(); //CHOOSES PROPER CLICK HANDLING BASED ON CHARACTER

            flipSpriteBasedOnMousePos(lookPos.x); //FLIP SPRITE BASED ON MOUSE POSITION

            executeSpriteFlipBasedOnCharacter(); //APPLY FLIP SPRITE VARIABLE DETERMINED IN flipSpriteBasedOnMousePos()

             //RELOADS VENS BOW

            reloadAnimationLogic();

            calculateAngle(lookPos);

            roverGunSpriteSwapLogic();

            shootingLogic();

            allowFlight(); //FALSE FOR ROVER ON SPECIAL, TRUE FOR ALL OTHERS

            putGunAway();

            handleJump(); //HANDLES JUMP LOGIC, APPLIES TO ALL CHARACTERS
            handleFlight(); //LIKE JUMP, APPLIED TO ALL
            duringAndEndFlightAndJump(); //FINISHES ABOVE 2 METHODS

            specialLogic();

            if (GameInputManager.GetKeyUp("Special") || amtFuel < 1)
            {
                if (StatsHolder.characterSelected != 3 && StatsHolder.characterSelected != 0 && StatsHolder.characterSelected != 4)
                {
                    special = false;
                }
                specialMult = 1;
            }

            engageDance();

            stopDance();
            startStopFocus();
            checkForDancingAchievement();

           
            if (StatsHolder.characterSelected == 1 && special)
            {
                isFlying = false;
            }

            setAnimatorBools();

            manageConstantUseSpecials();

            targetVelocity = move * maxSpeed * sprintMultiplier * specialMult;
            //Debug.Log (animator.GetBool(grounded));
        }
        jetpacking = isFlying;
    }
   

    private IEnumerator unhideOwl()
    {
        yield return new WaitForSeconds(3);
        owlAttackUsed = false;
        birdAnimator.SetBool("hidden", false);
        animator.SetBool("birdFired", false);
    }


    public void subtractFocusFuelEvenly()
    {
        if (amtFuel < 264)
        {
            amtFuel += 0.5f;
        }
    }
    public void fireAGrenade(Vector3 pos)
    {

        float neg = 1;
        if (flipSprite)
        {
            neg = -1;
        }
        if (isLocalPlayer)
        {
            CmdFireGrenade(pos, GetName.userName, neg);
        }
    }
    public void initiateNeptunianSpecial()
    {
        if(isLocalPlayer){
            special = false;
            animator.SetBool("bubble", false);
            CmdSpawnBubble(transform.position, GetName.userName, flipSprite);
        }
    }
    public void phaseOut()
    {
        animator.SetBool("dead", true);
        dead = true;
        arm.SetActive(false);
        shooting = false;
        amtFuel = 264;
    }
    public void phaseIn()
    {

        setStatsToDefault();
        animator.SetBool("dead", false);
        animator.SetInteger("costume", StatsHolder.currentSelectedSkin);
        dead = false;
        inputLock = false;
        fallenOff = false;
        shooting = false;
        isFlying = false;
        special = false;
        isSmoking = false;
        if (StatsHolder.characterSelected == 1 || StatsHolder.characterSelected == 3 || StatsHolder.characterSelected == 2 || StatsHolder.characterSelected == 0)
        {
            arm.SetActive(true);
        }
    }
    public void setStatsToDefault()
    {
        normBurst = false;
        canPutGunAway = true;
        sprintMultiplier = 1;
        specialMult = 1;
        if (StatsHolder.characterSelected == 1)
        {
            shotTimeMax = 70;
        }
        if (StatsHolder.characterSelected == 3)
        {
            shotTimeMax = 85;
        }
        if (StatsHolder.characterSelected == 2)
        {
            shotTimeMax = 40;

        }
        if (StatsHolder.characterSelected == 0)
        {
            shotTime = 10;
        }
    }
    public void refillFuel(int pos)
    {
        if (!isServer)
        {
            return;
        }

        Debug.Log("REFILLING FUEL");
        SpawnNewStar(pos);
        RpcUpdateFuelBarWithPlasmaCount();
        //fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 40);
    }
    public void refillFuelLocalPlayerAuthority(int pos)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdCallSpawnNewStar(pos);
        collectedStars++;
        amtFuel = 264;
    }
    public int collectAndResetStarCount()
    {
        int temp = collectedStars;
        collectedStars = 0;
        return temp;
    }
    public void usedBasicAttack()
    {
        basicAttacks++;
    }
    public void usedSpecialAttack() {
        specialAttacks++;
    }
    public int resetAndCollectBasicAttacks()
    {
        int temp = basicAttacks;
        basicAttacks = 0;
        return temp;
    }
    public int resetAndCollectSpecialAttacks()
    {
        int temp = specialAttacks;
        specialAttacks = 0;
        return temp;
    }
    public bool amILocalPlayer()
    {
        return isLocalPlayer;
    }
    [Command]
    public void CmdCallSpawnNewStar(int pos)
    {
        SpawnNewStar(pos);
    }
    public void SpawnNewStar(int pos)
    {
        if (!isServer)
        {
            return;
        }

        PlasmaManager plasmaPack = GameObject.FindGameObjectWithTag("PlasmaManager").GetComponent<PlasmaManager>();
        plasmaPack.SpawnNewPlasmaPack(pos);

    }
    public void refillFuelOnDeath()
    {
        if (!isServer)
        {
            return;
        }

        RpcUpdateFuelBar();
        //fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 40);
    }
    public void stopJupSpecial()
    {
        special = false;
    }
    public void freezePlayer()
    {
        spriteRenderer.enabled = false;
        animator.enabled = false;

    }

    [Command]
    void CmdFireShotty(Vector3 direct, Vector2 pos, string whoShot, float syncZRot)
    {

        var pellet1 = (GameObject)Instantiate(
            roverBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet2 = (GameObject)Instantiate(
            roverBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet3 = (GameObject)Instantiate(
            roverBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet4 = (GameObject)Instantiate(
            roverBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet5 = (GameObject)Instantiate(
            roverBulletPrefab,
            pos,
            bulletSpawn.rotation);

        Debug.Log("force");
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //Fire (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation, dir, 1000);

        //bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(pellet2);
        NetworkServer.Spawn(pellet1);

        NetworkServer.Spawn(pellet3);
        NetworkServer.Spawn(pellet4);
        NetworkServer.Spawn(pellet5);

        RpcAddForceOnAllPellets(pos, direct, whoShot);
        #if UNITY_SERVER || UNITY_EDITOR
        AddForceOnAllPelletsServer(pellet1, pellet2, pellet3, pellet4, pellet5, direct, whoShot);
        #endif


        Destroy(pellet1, 0.55f);
        Destroy(pellet2, 0.55f);
        Destroy(pellet3, 0.55f);
        Destroy(pellet4, 0.55f);
        Destroy(pellet5, 0.55f);
    }
    [Command]
    void CmdFireArrow1(Vector3 direct, Vector2 pos, string whoShot, Quaternion rot, float syncZRot)
    {

        var pellet1 = (GameObject)Instantiate(
            arrowPrefab,
            pos,
            rot);
        var pellet2 = (GameObject)Instantiate(
            arrowPrefab,
            pos,
            rot);
        var pellet3 = (GameObject)Instantiate(
            arrowPrefab,
            pos,
            rot);

        pellet1.transform.eulerAngles = new Vector3(
            pellet1.transform.eulerAngles.x,
            pellet1.transform.eulerAngles.y,
            pellet1.transform.eulerAngles.z + 20
        );


        pellet3.transform.eulerAngles = new Vector3(
            pellet3.transform.eulerAngles.x,
            pellet3.transform.eulerAngles.y,
            pellet3.transform.eulerAngles.z - 20
        );

        NetworkServer.Spawn(pellet1);
        NetworkServer.Spawn(pellet2);
        NetworkServer.Spawn(pellet3);
        RpcAddForceOnArrow(pos, direct, 1, pellet1.transform.rotation, whoShot);
        RpcAddForceOnArrow(pos, direct, 2, pellet2.transform.rotation, whoShot);
        RpcAddForceOnArrow(pos, direct, 3, pellet3.transform.rotation, whoShot);
        #if UNITY_SERVER || UNITY_EDITOR
        AddForceOnArrowServer(pellet1, direct, whoShot, 1);
        AddForceOnArrowServer(pellet2, direct, whoShot, 2);
        AddForceOnArrowServer(pellet3, direct, whoShot, 3);
        #endif
        Destroy(pellet1, 1.2f);
        Destroy(pellet2, 1.2f);
        Destroy(pellet3, 1.2f);

    }
    void plutoPunch(Vector3 direct, Vector2 pos, string whoShot)
    {
        armAnim.SetBool("punch", true);
        CmdPunch(direct, pos, whoShot);
        StartCoroutine("StopPunching");
    }
    [Command]
    void CmdPunch(Vector3 direct, Vector2 pos, string whoShot)
    {

    }
    [Command]
    void CmdFire(Vector3 direct, Vector2 pos, string whoShot, bool raged, float syncZRot)
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            pos,
            bulletSpawn.rotation);



        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //Fire (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation, dir, 1000);

        //bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);
        RpcAddForceOnAllTest(direct, pos, raged, whoShot);
        #if UNITY_SERVER || UNITY_EDITOR
        AddForceOnAllServer(bullet, direct, whoShot, raged);
        #endif


        if (!raged)
        {
            Destroy(bullet, 0.9f);
        }
        else
        {
            Destroy(bullet, 1.0f);
        }
    }
    [Command]
    void CmdFireBigWaves(Vector2 pos, string whoShot)
    {
        var bullet = (GameObject)Instantiate(
            fireballGravPrefab,
            new Vector2(pos.x + 0.8f, pos.y - 0.4f),
            Quaternion.Euler(0, 0, 0));
        var bullet2 = (GameObject)Instantiate(
           fireballGravPrefab,
           new Vector2(pos.x - 0.8f, pos.y - 0.4f),
           Quaternion.Euler(0, 0, 0));
        var bullet3 = (GameObject)Instantiate(
            fireballGravPrefab,
            new Vector2(pos.x - 0.1f, pos.y),
            Quaternion.Euler(0, 0, 0));
        var bullet4 = (GameObject)Instantiate(
           fireballGravPrefab,
           new Vector2(pos.x + 0.1f, pos.y),
           Quaternion.Euler(0, 0, 0));

        NetworkServer.Spawn(bullet);
        NetworkServer.Spawn(bullet2);
        NetworkServer.Spawn(bullet3);
        NetworkServer.Spawn(bullet4);
        RpcAddForceOnAllWavesTest(pos, whoShot);
        RpcAddForceOnAllWavesTestExtra(pos, whoShot);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnAllWavesServer(bullet, bullet2, whoShot);
        AddForceOnAllWavesServerExtra(bullet3, bullet4, whoShot);
#endif
        Destroy(bullet, 5.0f);
        Destroy(bullet2, 5.0f);
        Destroy(bullet3, 5.0f);
        Destroy(bullet4, 5.0f);

    }
    [Command]
    void CmdFireLittleWaves(Vector2 pos, string whoShot)
    {
        var bullet = (GameObject)Instantiate(
            fireballGravPrefab,
            new Vector2(pos.x + 0.8f, pos.y - 0.4f),
            Quaternion.Euler(0, 0, 0));
        var bullet2 = (GameObject)Instantiate(
           fireballGravPrefab,
           new Vector2(pos.x - 0.8f, pos.y - 0.4f),
           Quaternion.Euler(0, 0, 0));

        NetworkServer.Spawn(bullet);
        NetworkServer.Spawn(bullet2);
        RpcAddForceOnAllWavesTest(pos, whoShot);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnAllWavesServer(bullet, bullet2, whoShot);
#endif

        Destroy(bullet, 5.0f);
        Destroy(bullet2, 5.0f);

    }
    [Command]
    void CmdNepSlash(Vector2 pos, string whoShot, bool flipped)
    {
        float xValue = 0.6f;
        float rotVal = 0;
        if (flipped)
        {
            xValue = -0.6f;
            rotVal = 180;
        }
        var bullet = (GameObject)Instantiate(
            fireballGravPrefab,
            new Vector2(pos.x + xValue, pos.y - 0.4f),
            Quaternion.Euler(0, 0, rotVal));


        NetworkServer.Spawn(bullet);

        RpcAddForceOnNepSlash(pos, whoShot, flipped);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnNepSlashServer(bullet, whoShot, flipped);
#endif
        Destroy(bullet, 2.6f);

    }
    [Command]
    void CmdSpawnBubble(Vector2 pos, string whoShot, bool flipped)
    {
        float xValue = 1.0f;

        if (flipped)
        {
            xValue = -1.0f;
           
        }
        var bullet = (GameObject)Instantiate(
            grenadePrefab,
            new Vector2(pos.x + xValue, pos.y + 1.8f),
            Quaternion.Euler(-90, 0, 0));


        NetworkServer.Spawn(bullet);

        // RpcAddForceOnNepSlash(pos, whoShot, flipped);
#if UNITY_SERVER || UNITY_EDITOR
        ConfigureBubble(bullet, whoShot);
#endif
        Destroy(bullet, 5.0f);

    }
    
    
    [ClientRpc]
    void RpcAddForceOnNepSlash(Vector2 pos, string whoShot, bool flipped)
    {
        float xValue = 0.6f;
        float direction = 1;
        float rotVal = 0;
        if (flipped)
        {
            xValue = -0.6f;
            direction = -1;
            rotVal = 180;
        }
        var bullet = (GameObject)Instantiate(
           localFireballGravPrefab,
           new Vector2(pos.x + xValue, pos.y - 0.4f), 
           Quaternion.Euler(0, rotVal,0));


        LocalWavePrefab lwp = bullet.GetComponent<LocalWavePrefab>();
            Destroy(bullet, 2.6f);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 0.0f) * 500);
            lwp.whoShot = whoShot;
        if (isLocalPlayer)
        {
            animator.SetBool("attack", true);
        }

    }
    [ClientRpc]
    void RpcAddForceOnAllWavesTest(Vector2 pos, string whoShot)
    {
        var bullet = (GameObject)Instantiate(
           localFireballGravPrefab,
           new Vector2(pos.x + 0.8f, pos.y - 0.4f),
           Quaternion.Euler(0, 0, 0));
        var bullet2 = (GameObject)Instantiate(
         localFireballGravPrefab,
         new Vector2(pos.x - 0.8f, pos.y - 0.4f),
         Quaternion.Euler(0, 0, 0));

        LocalFireball lwp = bullet.GetComponent<LocalFireball>();
        Destroy(bullet, 5.0f);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.3f) * 400);
        lwp.whoShot = whoShot;

        LocalFireball lwp2 = bullet2.GetComponent<LocalFireball>();
        Destroy(bullet2, 5.0f);
        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.3f) * 400);
        lwp2.whoShot = whoShot;

    }

    [ClientRpc]
    void RpcAddForceOnAllWavesTestExtra(Vector2 pos, string whoShot)
    {
        var bullet = (GameObject)Instantiate(
           localFireballGravPrefab,
           new Vector2(pos.x - 0.1f, pos.y),
           Quaternion.Euler(0, 0, 0));
        var bullet2 = (GameObject)Instantiate(
         localFireballGravPrefab,
         new Vector2(pos.x + 0.1f, pos.y),
         Quaternion.Euler(0, 0, 0));

        LocalFireball lwp = bullet.GetComponent<LocalFireball>();
        Destroy(bullet, 5.0f);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.6f, 1) * 500);
        lwp.whoShot = whoShot;

        LocalFireball lwp2 = bullet2.GetComponent<LocalFireball>();
        Destroy(bullet2, 5.0f);
        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.6f, 1) * 500);
        lwp2.whoShot = whoShot;

    }
    
    [Command] 
    void CmdAddPlasmaParticles()
    {
        RpcAddPlasmaParticles();
    }
    [ClientRpc]
    void RpcAddPlasmaParticles()
    {
        GameObject particlePrefab = Instantiate(normanPlasmaParticles);
        particlePrefab.transform.parent = gameObject.transform;
       particlePrefab.transform.localPosition = new Vector3(0,1.07f,0);

    }
    [Command]
    void CmdFireOwl(Vector3 direct, Vector2 pos, string whoShot, bool flipped)
    {
        var bullet = (GameObject)Instantiate(
            owlPrefab,
            pos,
            bulletSpawn.rotation);

        NetworkServer.Spawn(bullet);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnOwlServer(bullet, direct, whoShot, flipped);
#endif
        RpcAddForceOnOwl(pos, direct, whoShot, flipped);
       
        Destroy(bullet, 3.0f);
    }
    [Command]
    void CmdFireGrenade(Vector2 pos, string whoShot, float neg)
    {
        var bullet = (GameObject)Instantiate(
            grenadePrefab,
            pos,
            bulletSpawn.rotation);

        NetworkServer.Spawn(bullet);
        //RpcAddForceOnAllGrenade(bullet, whoShot, neg);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnAllGrenadeServer(bullet, whoShot, neg);
#endif


        Destroy(bullet, 5.0f);
    }
    [Command]
    void CmdFireball(Vector3 direct, Vector2 pos, string whoShot, float syncZRot)
    {
        var bullet = (GameObject)Instantiate(
            fireballPrefab,
            pos,
            bulletSpawn.rotation);


        Debug.Log("force");
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //Fire (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation, dir, 1000);

        //bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);
        RpcAddForceOnAllFireball(pos, direct, whoShot, syncZRot);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnAllFireballServer(bullet, direct, whoShot);
#endif


        Destroy(bullet, 5.0f);
    }
    [Command]
    void CmdFireDragon(Vector3 direct, Vector2 pos, string whoShot, bool flipped)
    {
        var bullet = (GameObject)Instantiate(
           dragonPrefab,
           pos,
           bulletSpawn.rotation);

        NetworkServer.Spawn(bullet);
#if UNITY_SERVER || UNITY_EDITOR
        AddForceOnOwlServer(bullet, direct, whoShot, flipped);
#endif
        RpcAddForceOnDragon(pos, direct, whoShot, flipped);

        Destroy(bullet, 3.0f);
    }
    [ClientRpc]
    void RpcUpdateFuelBar()
    {
        if (isLocalPlayer)
        {
            amtFuel = 264;
        }
    }
    [ClientRpc]
    void RpcUpdateFuelBarWithPlasmaCount()
    {
        if (isLocalPlayer)
        {
            Debug.Log("INCREMENT STARS");
            collectedStars++;
            amtFuel = 264;
        }
    }
    //[ClientRpc]
    //void RpcAddForceOnAll(GameObject bullet2, Vector3 direct, string whoFired, bool isRaged, float syncZRot)
    //{
    //    //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
    //    //Vector3 dir = (lastValidPos - sp).normalized;
    //    //GameObject bullet = ClientScene.FindLocalObject(bullet_id);
    //    if (!isRaged)
    //    {
    //        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 600);
    //        bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
    //    }
    //    else
    //    {
    //        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
    //        bullet2.GetComponent<TrailRenderer>().enabled = true;
    //        bullet2.GetComponent<SpriteRenderer>().color = new Color(0.486f, 0.76f, 0.96f);
    //    }

    //    bullet2.GetComponent<Bullet>().whoFiredMe = whoFired;
    //    //FIND SILVER RAIN
    //    Vector3 armZRotation = new Vector3(0, 0, syncZRot);
    //    arm.transform.localRotation = arm.transform.localRotation = Quaternion.Euler(armZRotation);
    //    armAnim.SetBool("shoot", true);
    //}
    [ClientRpc]
    void RpcAddForceOnAllTest(Vector3 direct, Vector2 pos, bool isRaged, string whoShot)
    {
      
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        var bullet = (GameObject)Instantiate(
           localBulletPrefab,
           pos,
           bulletSpawn.rotation);
        LocalNormanBullet lnb = bullet.GetComponent<LocalNormanBullet>();
        if (!isRaged)
        {
           
            Destroy(bullet, 0.9f);
            bullet.GetComponent<Rigidbody2D>().AddForce(direct * 650);
            bullet.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        }
        else
        {
            lnb.setColor();
            Destroy(bullet, 1.0f);
            bullet.GetComponent<Rigidbody2D>().AddForce(direct * 800);
            bullet.GetComponent<TrailRenderer>().enabled = true;
            bullet.GetComponent<SpriteRenderer>().color = new Color(0.486f, 0.76f, 0.96f);
        }
        lnb.whoShot = whoShot;
        armAnim.SetBool("shoot", true);
    }
    [ClientRpc]
    void RpcAddForceOnAllFireball(Vector2 pos, Vector3 direct, string whoFired, float syncZRot)
    {

        var bullet2 = (GameObject)Instantiate(
         localBulletPrefab,
         pos,
         bulletSpawn.rotation);

        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 400);
        bullet2.GetComponent<LocalBulletBase>().whoShot = whoFired;
        Destroy(bullet2, 5.0f);
    }
    [ClientRpc]
    void RpcAddForceOnAllGrenade(GameObject bullet2, string whoFired, float neg)
    {
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);


        // bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(45 * neg * 1.5f, 60) * 10);
     //   bullet2.GetComponent<Bullet>().whoFiredMe = whoFired;
        //bullet2.GetComponent<Bullet>().bulletType = 5;

    }
    
    [ClientRpc]
    void RpcAddForceOnOwl(Vector2 pos, Vector3 direct, string whoFired, bool flipped)
    {
        var bullet2 = (GameObject)Instantiate(
          localOwlPrefab,
          pos,
          bulletSpawn.rotation);
        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
        bullet2.GetComponent<LocalBulletBase>().whoShot = whoFired;
        if (flipped)
        {
            bullet2.GetComponent<SpriteRenderer>().flipY = true;
        }
        Destroy(bullet2, 3.0f);

    }
    [ClientRpc]
    void RpcAddForceOnDragon(Vector2 pos, Vector3 direct, string whoFired, bool flipped)
    {
        var bullet2 = (GameObject)Instantiate(
          localDragonPrefab,
          pos,
          bulletSpawn.rotation);
        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
        bullet2.GetComponent<LocalBulletBase>().whoShot = whoFired;
        if (flipped)
        {
            bullet2.GetComponent<SpriteRenderer>().flipY = true;
        }
        Destroy(bullet2, 3.0f);

    }
    
    [ClientRpc]
    void RpcAddForceOnArrow(Vector2 pos, Vector3 direct, int numArrow, Quaternion rot, string whoShot)
    {
        var arrow1 = (GameObject)Instantiate(
             localBulletPrefab,
             pos,
             rot);
        float _x = direct.x * Mathf.Cos(-0.4f) - direct.y * Mathf.Sin(-0.4f);
        float _y = direct.x * Mathf.Sin(-0.4f) + direct.y * Mathf.Cos(-0.4f);
        if (numArrow == 2)
        {
            _x = direct.x * Mathf.Cos(0.0f) - direct.y * Mathf.Sin(0.0f);
            _y = direct.x * Mathf.Sin(0.0f) + direct.y * Mathf.Cos(0.0f);
        }
        if (numArrow == 1)
        {
            _x = direct.x * Mathf.Cos(0.4f) - direct.y * Mathf.Sin(0.4f);
            _y = direct.x * Mathf.Sin(0.4f) + direct.y * Mathf.Cos(0.4f);
        }

        Vector2 firstOne = new Vector2(_x, _y);

        arrow1.GetComponent<LocalBulletBase>().whoShot = whoShot;
        arrow1.GetComponent<Rigidbody2D>().AddForce(firstOne * 450);

        Destroy(arrow1, 1.2f);
        armAnim.SetBool("shoot", true);
    }
    
    [ClientRpc]
    void RpcAddForceOnAllPellets(Vector2 pos, Vector3 direct, string whoFired)
    {
        var pellet1 = (GameObject)Instantiate(
          localBulletPrefab,
          pos,
          bulletSpawn.rotation);
        var pellet2 = (GameObject)Instantiate(
            localBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet3 = (GameObject)Instantiate(
            localBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet4 = (GameObject)Instantiate(
            localBulletPrefab,
            pos,
            bulletSpawn.rotation);
        var pellet5 = (GameObject)Instantiate(
            localBulletPrefab,
            pos,
            bulletSpawn.rotation);

        float _x = direct.x * Mathf.Cos(-0.2f) - direct.y * Mathf.Sin(-0.2f);
        float _y = direct.x * Mathf.Sin(-0.2f) + direct.y * Mathf.Cos(-0.2f);
        Vector2 firstOne = new Vector2(_x, _y);
        float _x2 = direct.x * Mathf.Cos(-0.1f) - direct.y * Mathf.Sin(-0.1f);
        float _y2 = direct.x * Mathf.Sin(-0.1f) + direct.y * Mathf.Cos(-0.1f);



        float _x4 = direct.x * Mathf.Cos(0.2f) - direct.y * Mathf.Sin(0.2f);
        float _y4 = direct.x * Mathf.Sin(0.2f) + direct.y * Mathf.Cos(0.2f);

        float _x5 = direct.x * Mathf.Cos(0.1f) - direct.y * Mathf.Sin(0.1f);
        float _y5 = direct.x * Mathf.Sin(0.1f) + direct.y * Mathf.Cos(0.1f);

        pellet3.GetComponent<Rigidbody2D>().AddForce(direct * 600);
       
        pellet1.GetComponent<Rigidbody2D>().AddForce(firstOne * 650);
       
        pellet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(_x2, _y2) * 700);
    
        pellet4.GetComponent<Rigidbody2D>().AddForce(new Vector2(_x4, _y4) * 650);
     
        pellet5.GetComponent<Rigidbody2D>().AddForce(new Vector2(_x5, _y5) * 600);
        pellet1.GetComponent<LocalBulletBase>().whoShot = whoFired;
        pellet2.GetComponent<LocalBulletBase>().whoShot = whoFired;
        pellet3.GetComponent<LocalBulletBase>().whoShot = whoFired;
        pellet4.GetComponent<LocalBulletBase>().whoShot = whoFired;
        pellet5.GetComponent<LocalBulletBase>().whoShot = whoFired;
        Destroy(pellet1, 0.55f);
        Destroy(pellet2, 0.55f);
        Destroy(pellet3, 0.55f);
        Destroy(pellet4, 0.55f);
        Destroy(pellet5, 0.55f);
 
    }
#if UNITY_SERVER || UNITY_EDITOR
    void ConfigureBubble(GameObject bullet, string whoFired)
    {
        bullet.GetComponent<ServerBubble>().whoFiredMe = whoFired;
    }
    void AddForceOnNepSlashServer(GameObject bullet, string whoFired, bool flipped)
    {
        float direction = 1;
        if (flipped)
        {
            direction = -1;
        }
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 0.0f) * 500);
        bullet.GetComponent<ServerFireWave>().whoFiredMe = whoFired;


    }
    void AddForceOnOwlServer(GameObject bullet2, Vector3 direct, string whoFired, bool flipped)
    {
        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;
        if (flipped)
        {
            bullet2.GetComponent<SpriteRenderer>().flipY = true;
        }

    }
    void AddForceOnAllGrenadeServer(GameObject bullet2, string whoFired, float neg)
    {
        // bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(45 * neg * 0.8f, 60) * 10);
        //bullet2.GetComponent<Bullet>().whoFiredMe = whoFired;
        // bullet2.GetComponent<Bullet>().bulletType = 5;
    }
    void AddForceOnAllFireballServer(GameObject bullet2, Vector3 direct, string whoFired)
    {
        // bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 400);
        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;
    }
    void AddForceOnAllServer(GameObject bullet2, Vector3 direct, string whoFired, bool isRaged)
    {
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);
        if (!isRaged)
        {
            bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 650);
            bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        }
        else
        {
            bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
            bullet2.GetComponent<SpriteRenderer>().color = new Color(0.486f, 0.76f, 0.96f);
        }

        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;

    }
    void AddForceOnAllWavesServer(GameObject bullet, GameObject bullet2, string whoFired)
    {

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.3f) * 400);
        bullet.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;

        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.3f) * 400);
        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;

    }
    void AddForceOnAllWavesServerExtra(GameObject bullet, GameObject bullet2, string whoFired)
    {

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.6f, 1) * 500);
        bullet.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;

        bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.6f, 1) * 500);
        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;

    }
    void AddForceOnArrowServer(GameObject arrow1, Vector3 direct, string whoFired, int numArrow)
    {
        float _x = direct.x * Mathf.Cos(-0.4f) - direct.y * Mathf.Sin(-0.4f);
        float _y = direct.x * Mathf.Sin(-0.4f) + direct.y * Mathf.Cos(-0.4f);
        if (numArrow == 2)
        {
            _x = direct.x * Mathf.Cos(0.0f) - direct.y * Mathf.Sin(0.0f);
            _y = direct.x * Mathf.Sin(0.0f) + direct.y * Mathf.Cos(0.0f);
        }
        if (numArrow == 1)
        {
            _x = direct.x * Mathf.Cos(0.4f) - direct.y * Mathf.Sin(0.4f);
            _y = direct.x * Mathf.Sin(0.4f) + direct.y * Mathf.Cos(0.4f);
        }

        Vector2 firstOne = new Vector2(_x, _y);

        arrow1.GetComponent<Rigidbody2D>().AddForce(firstOne * 450);
        arrow1.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;
    }
    void AddForceOnAllPelletsServer(GameObject pellet1, GameObject pellet2, GameObject pellet3, GameObject pellet4,
    GameObject pellet5, Vector3 direct, string whoFired)
    {

        float _x = direct.x * Mathf.Cos(-0.2f) - direct.y * Mathf.Sin(-0.2f);
        float _y = direct.x * Mathf.Sin(-0.2f) + direct.y * Mathf.Cos(-0.2f);
        Vector2 firstOne = new Vector2(_x, _y);
        float _x2 = direct.x * Mathf.Cos(-0.1f) - direct.y * Mathf.Sin(-0.1f);
        float _y2 = direct.x * Mathf.Sin(-0.1f) + direct.y * Mathf.Cos(-0.1f);



        float _x4 = direct.x * Mathf.Cos(0.2f) - direct.y * Mathf.Sin(0.2f);
        float _y4 = direct.x * Mathf.Sin(0.2f) + direct.y * Mathf.Cos(0.2f);

        float _x5 = direct.x * Mathf.Cos(0.1f) - direct.y * Mathf.Sin(0.1f);
        float _y5 = direct.x * Mathf.Sin(0.1f) + direct.y * Mathf.Cos(0.1f);

        pellet3.GetComponent<Rigidbody2D>().AddForce(direct * 600);
        pellet3.GetComponent<ServerBulletRover>().whoFiredMe = whoFired;
        pellet1.GetComponent<Rigidbody2D>().AddForce(firstOne * 650);
        pellet1.GetComponent<ServerBulletRover>().whoFiredMe = whoFired;
        pellet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(_x2, _y2) * 700);
        pellet2.GetComponent<ServerBulletRover>().whoFiredMe = whoFired;
        pellet4.GetComponent<Rigidbody2D>().AddForce(new Vector2(_x4, _y4) * 650);
        pellet4.GetComponent<ServerBulletRover>().whoFiredMe = whoFired;
        pellet5.GetComponent<Rigidbody2D>().AddForce(new Vector2(_x5, _y5) * 600);
        pellet5.GetComponent<ServerBulletRover>().whoFiredMe = whoFired;

    }
    #endif
    public void ServerNewSpawn(PlayerController oldPlayer, int which)
    {

        Debug.Log("SPAWNING A NEW PLAYER");
        CmdServerNewSpawn(oldPlayer.gameObject, which);

    }
    [Command]
    void CmdServerNewSpawn(GameObject oldPlayer, int which)
    {
        var conn = oldPlayer.GetComponent<PlayerController>().connectionToClient;
        Destroy(oldPlayer);
        //var newPlayer = Instantiate<GameObject> (normanPrefab);
        Vector3 spawnPoint = Vector3.zero;
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        // If there is a spawn point array and the array is not empty, pick one at random
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Debug.Log("HAS SPAWN POINTS");
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }
        else
        {
            Debug.Log("No SPAWN POINTS");
        }

        // Set the player’s position to the chosen spawn point

        transform.position = spawnPoint;

        NetworkServer.ReplacePlayerForConnection(conn, Instantiate(playerReferences.getCharacter(which), spawnPoint, Quaternion.identity) as GameObject);


    }


    [ClientRpc]
    void RpcKillOldPlayer(GameObject oldPlayer)
    {
        //	Debug.Log ("I CAN RUN THIS AT LEAST");
        //Debug.Log (newPlayer.name);
        /*if (GetName.userName == newPlayer.name) {
                newPlayer.GetComponent<Health> ().resetTimer ();
                Debug.Log ("RESET TIMER PLAYER BA CAW");
            Debug.Log ("RESET TIMER PLAYER BA CAW");
            Debug.Log ("RESET TIMER PLAYER BA CAW");
            Debug.Log ("RESET TIMER PLAYER BA CAW");
        }*/
        //Destroy (oldPlayer);
        //	NetworkServer.Destroy (oldPlayer);
    }
    [Command]
    void CmdUpdateFireballSprite(int which)
    {
        currentReloadStage = which;

    }

    void UpdateFireballSprite(int was, int which)
    {
        if (!isLocalPlayer)
        {
            if (!isServer)
            {
                currentReloadStage = which;
            }
            if (roverGun == null)
            {
                roverGun = arm.GetComponent<SpriteRenderer>();
            }


            if (roverGun != null)
            {

                roverGun.sprite = jupArm.getArmSprite(5 - which);
            }
        }
    }
    public void setUpGoldCostume()
    {
        Debug.Log("SETTING UP THE GOLD?");
        goldParticles.SetActive(true);
    }
    public void turnOffGoldCostume()
    {
        goldParticles.SetActive(false);
    }
    IEnumerator StopBursting()
    {
        yield return new WaitForSeconds(3);
        shooting = false;
        canPutGunAway = true;
       
        normBurst = false;
        shotTimeMax = normShotMax;
        special = false;
    }
    IEnumerator StopPunching()
    {
        yield return new WaitForSeconds(0.1f);
        armAnim.SetBool("punch", false);
    }
    //SCRIPTS FOR HEALTH PICKUP EFFECTS
    public void endPack(GameObject heartObject)
    {
        CmdEndThisPack(heartObject.transform.parent.gameObject); // UNET CAN ONLY PASS OBJECTS W/ NETWORK IDENTITY
    }
    [Command]
    void CmdEndThisPack(GameObject heartObject)
    {
        NetworkServer.Destroy(heartObject);
        float x = heartObject.transform.position.x;
        float y = heartObject.transform.position.y;
        RpcEndThisPack(x, y);
    }
    [ClientRpc]
    void RpcEndThisPack(float x, float y)
    {
        if (isLocalPlayer)
        {
            return;
        }
        var parts = (GameObject)Instantiate(
                         heartPopper,
                         new Vector2(x,y),
                         heartPopper.transform.rotation);
    }
    //SIMILAR SCRIPTS FOR PLASMA PICKUP EFFECTS
    public void endPlasmaPack(GameObject plasmaObject)
    {
        CmdEndThisPlasmaPack(plasmaObject.transform.parent.gameObject); // UNET CAN ONLY PASS OBJECTS W/ NETWORK IDENTITY
    }
    [Command]
    void CmdEndThisPlasmaPack(GameObject plasmaObject)
    {
        NetworkServer.Destroy(plasmaObject);
        float x = plasmaObject.transform.position.x;
        float y = plasmaObject.transform.position.y;
        RpcEndThisPack(x, y);
    }
    [ClientRpc]
    void RpcEndThisPlasmaPack(float x, float y)
    {
        if (isLocalPlayer)
        {
            return;
        }
        var parts = (GameObject)Instantiate(
                         plasmaPopper,
                         new Vector2(x, y),
                         plasmaPopper.transform.rotation);
    }
}