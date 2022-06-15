/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Cinemachine;
public abstract class PlayerBase : PhysicsObject
{
    float startDancingTime = 0;
    public GameObject normanPrefab;
    public GameObject roverPrefab;
    public GameObject venusianPrefab;
    public GameObject juppernautPrefab;

    public GameObject normanPlasmaParticles;
    private bool normBurst = false;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private float sprintMultiplier = 1;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool canPutGunAway = true;
    private bool isFlying = false;
    private bool isSmoking = false;
    private bool isBooting = false;

    private CinemachineVirtualCamera followCamera;

    [SerializeField] Sprite[] jupGunSprites = new Sprite[6];
    public Image fuelMeter;
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
    private float adjustedSmoothMovement = 0;


    //[SerializeField] Animatio roverAnim;
    [SerializeField] SpriteRenderer mapIdentifier;

    [SerializeField] GameObject focusBarHolder;
    [SerializeField] Animator focusBar;
    private bool previousFocus = false;

    [SerializeField] SpriteRenderer birdRenderer;
    [SerializeField] Animator birdAnimator;
    [SerializeField] GameObject owl;

    AudioSource audioData;

    private bool jetpackAudio = false;
    private bool owlAttackUsed = false;

    Vector3 aPosition1;

    [SerializeField] GameObject roverFireParticles;
    ParticleSystem roverFireParticleSystem;
    public float armPosMove = 0.31f;
    [SyncVar(hook = nameof(UpdateFireballSprite))]
    int currentReloadStage = 0;
    private NetworkStartPosition[] spawnPoints;

    private bool isInCrouch = false;

    bool canFly = true;

    [SerializeField] GameObject arm2;
    SpriteRenderer armRenderer2;

    bool flipSpriteRover = false;
    bool canFlipRover = false;

    [SerializeField] GameObject goldParticles;

    private bool left;
    private bool right;
    double horAxis = 0;
    Vector2 move;
    public virtual void startLogic()
    {

    }
    public override void OnStartLocalPlayer()
    {
        startLogic();
        GameInputManager.showKeys();
        mapIdentifier.color = new Color(82 / 255f, 1, 33 / 255f);
        if (StatsHolder.characterSelected == 1)
        {
            arm.SetActive(true);
            roverGun = arm.GetComponent<SpriteRenderer>();
            shotTimeMax = 70;
            roverFireParticleSystem = roverFireParticles.GetComponent<ParticleSystem>();
        }
        if (StatsHolder.characterSelected == 3)
        {
            arm.SetActive(true);
            roverGun = arm.GetComponent<SpriteRenderer>();
            shotTimeMax = 102;
            armRenderer2 = arm2.GetComponent<SpriteRenderer>();
        }
        if (StatsHolder.characterSelected == 2)
        {
            shotTimeMax = 40;
            armPosMove = 0.54f;
            arm.SetActive(true);
        }
        HUD.SetActive(true);
        armRenderer = arm.GetComponent<SpriteRenderer>();
        healthScript = GetComponent<Health>();
        fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 60.6f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        followCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
        followCamera.Follow = gameObject.transform;
        animator.SetInteger("costume", StatsHolder.currentSelectedSkin);
    }
    public void updateJupGun()
    {
        if (StatsHolder.characterSelected == 3)
        {
            if (shotTime < 85 && shotTime > 68 && currentReloadStage == 5)
            {
                roverGun.sprite = jupGunSprites[1];
                CmdUpdateFireballSprite(4);
                currentReloadStage = 4;
            }
            if (shotTime < 68 && shotTime > 51 && currentReloadStage == 4)
            {
                roverGun.sprite = jupGunSprites[2];
                CmdUpdateFireballSprite(3);
                currentReloadStage = 3;
            }
            if (shotTime < 51 && shotTime > 34 && currentReloadStage == 3)
            {
                roverGun.sprite = jupGunSprites[3];
                CmdUpdateFireballSprite(2);
                currentReloadStage = 2;
            }
            if (shotTime < 34 && shotTime > 17 && currentReloadStage == 2)
            {
                roverGun.sprite = jupGunSprites[4];
                CmdUpdateFireballSprite(1);
                currentReloadStage = 1;
            }
            if (shotTime <= 0 && currentReloadStage == 1)
            {
                roverGun.sprite = jupGunSprites[5];
                CmdUpdateFireballSprite(0);
                currentReloadStage = 0;
            }
        }
    }
    public void lockIfHologramFade()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade") || animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade 0") || animator.GetCurrentAnimatorStateInfo(0).IsName("HologramFade 0 0"))
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
    }
    public void setJupGrenadeArm()
    {
        if (StatsHolder.characterSelected == 3)
        {
            Vector3 pos2 = arm2.transform.localPosition;
            pos2.x = 0.87f;
            arm2.transform.localPosition = pos2;
            // armRenderer2.flipY = false;
            armRenderer2.flipX = true;
        }
    }
    public void setJupGrenadeArmBack() {
        if (StatsHolder.characterSelected == 3)
        {
            Vector3 pos2 = arm2.transform.localPosition;
            pos2.x = -0.87f;
            arm2.transform.localPosition = pos2;
            armRenderer2.flipX = false;
            //armRenderer2.flipY = false;
        }
    }
    public void setArmBack()
    {
        Vector3 pos = arm.transform.localPosition;
        pos.x = armPosMove;
        arm.transform.localPosition = pos;
        armRenderer.flipY = false;
        armLeft = false;
    }
    public void setArmForawrd()
    {
        Vector3 pos = arm.transform.localPosition;
        pos.x = -(armPosMove);
        arm.transform.localPosition = pos;
        armRenderer.flipY = true;
        armLeft = true;
    }
    public void setArmPositions()
    {
        if (flipSprite != lastFlip && !shooting)
        {
            lastFlip = flipSprite;
            if (flipSprite)
            {
                setJupGrenadeArm();
            }
            else
            {

                setArmBack();
                setJupGrenadeArmBack();
            }
        }
    }
    public void checkForLostInSpace()
    {
        if (gameObject.transform.position.y < -50)
        {
            healthScript.fallOff();
            if (isLocalPlayer)
            {
                GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(0);
            }
        }
    }
    public void moveLeftAndRight()
    {
        right = GameInputManager.GetKey("Right");
        left = GameInputManager.GetKey("Left");

        if (left && right)
        {
            horAxis = 0;
        }
        else if (left || right)
        {
            if (left && System.Math.Abs(horAxis - -1) > 0.01)
            {

                if (horAxis > 0.01)
                {

                    horAxis = 0;
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
                    horAxis = 0;
                }
                else
                {
                    horAxis += Time.deltaTime * 5;
                }
            }
        }
        else
        {

            if (horAxis > 0.01)
            {
                if (horAxis > Time.deltaTime * 10)
                {
                    horAxis -= Time.deltaTime * 10;
                }
                else
                {
                    horAxis = 0;
                }
            }
            else if (horAxis < 0.01)
            {
                if (horAxis < Time.deltaTime * -20)
                {
                    horAxis += Time.deltaTime * 20;
                }
                else
                {
                    horAxis = 0;
                }
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
        move.x = (float)horAxis;
    }
    public void checkStopFlight()
    {
        if (isInFlight)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), aPosition1, 10 * Time.deltaTime);
            if (Vector2.Distance(transform.position, aPosition1) < 0.5)
            {
                velocity.y = 0;
                shouldMove = true;
                isInFlight = false;

            }
        }
    }
    public virtual void fireWeapon()
    {

    }
    public void fireWeaponGeneral()
    {
        if (shooting)
        {
            if (shotTime < 0)
            {
                shotTime = shotTimeMax;

                //  Fire ();
                Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = (lastValidPos - sp).normalized;
                Vector3 shootPosition = bulletSpawn.transform.position;

                if (flipSprite)
                {
                    shootPosition = bulletSpawn2.position;
                }
                if (StatsHolder.characterSelected == 0)
                {
                    canPutGunAway = true;
                    if (!GameInputManager.GetKey("Fire1") && shooting && !normBurst)
                    {
                        shooting = false;
                        arm.SetActive(false);
                    }
                    CmdFire(dir, shootPosition, GetName.userName, special, arm.transform.localEulerAngles.z);
                }
                else if (StatsHolder.characterSelected == 2)
                {
                    canPutGunAway = true;
                    if (!GameInputManager.GetKey("Fire1") && shooting)
                    {
                        shooting = false;

                    }
                    CmdFireArrow1(dir, shootPosition, GetName.userName, arm.transform.rotation, arm.transform.localEulerAngles.z);

                }
                else if (StatsHolder.characterSelected == 1)
                {
                    CmdFireShotty(dir, shootPosition, GetName.userName, arm.transform.localEulerAngles.z);
                }
                else if (StatsHolder.characterSelected == 4)
                {
                    CmdFireball(dir, shootPosition, GetName.userName, arm.transform.localEulerAngles.z);
                    roverGun.sprite = jupGunSprites[0];
                    CmdUpdateFireballSprite(5);
                    currentReloadStage = 5;
                }

            }
        }
    }
    protected override void ComputeVelocity()
    {

        updateJupGun();
        lockIfHologramFade();
        setArmPositions();
       
        if (!isLocked)
        {
            checkForLostInSpace();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (armLeft == false && lookPos.x > gameObject.transform.position.x || armLeft == true && lookPos.x < gameObject.transform.position.x)
            {
                lastValidPos = Input.mousePosition;
            }
            move = Vector2.zero;
            moveLeftAndRight();
            /* if (Input.GetButton("Fly"))
             {

             }

            // move.x = Input.GetAxis("Horizontal");
            if (isInCrouch && GameInputManager.GetKeyDown("Fire1") && !isInFlight)
            {
                aPosition1 = Camera.main.ScreenToWorldPoint(mousePos);
                isInFlight = true;
                isInCrouch = false;
                animator.SetBool("crouch", isInCrouch);
            }
            checkStopFlight();
            if (GameInputManager.GetKeyDown("Fire1") && !isInCrouch || normBurst && !shooting)
            {
                if (!shooting && StatsHolder.characterSelected == 0 && !dead)
                {

                    arm.SetActive(true);
                    if (StatsHolder.characterSelected == 0)
                    {
                        canPutGunAway = false;
                    }
                    /*  if (flipSprite) {
                    Vector3 pos = arm.transform.localPosition;
                    pos.x = -0.31f; 
                    arm.transform.localPosition = pos;
                    armRenderer.flipY = true;
                    armLeft = true;
                } else {
                    Vector3 pos = arm.transform.localPosition;
                    pos.x = 0.31f; 
                    arm.transform.localPosition = pos;
                    armRenderer.flipY = false;
                    armLeft = false;
                } 

                }
                shooting = true;

            }
            if (lookPos.x > gameObject.transform.position.x)
            {
                flipSprite = false;
            }
            if (lookPos.x <= gameObject.transform.position.x)
            {
                flipSprite = true;
            }

            if (StatsHolder.characterSelected != 1 || StatsHolder.characterSelected == 1 && !special || Mathf.Abs(velocity.x) < 0.001)
            {
                spriteRenderer.flipX = flipSprite;
                canFlipRover = false;
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

            if (StatsHolder.characterSelected == 2)
            {
                shotTime -= Time.deltaTime * 30;
                birdRenderer.flipX = flipSprite;
                birdAnimator.SetBool("right", flipSprite);
            }



            if (shooting || StatsHolder.characterSelected == 1 || StatsHolder.characterSelected == 3 || StatsHolder.characterSelected == 2)
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
                        if (StatsHolder.characterSelected == 3)
                        {
                            Vector3 pos2 = arm2.transform.localPosition;
                            pos2.x = 0.87f;
                            arm2.transform.localPosition = pos2;
                            armRenderer2.flipX = true;
                        }
                    }
                    else
                    {

                        Vector3 pos = arm.transform.localPosition;
                        pos.x = armPosMove;
                        arm.transform.localPosition = pos;
                        armRenderer.flipY = false;
                        armLeft = false;
                        if (StatsHolder.characterSelected == 3)
                        {
                            Vector3 pos2 = arm2.transform.localPosition;
                            pos2.x = -0.87f;
                            arm2.transform.localPosition = pos2;
                            armRenderer2.flipX = false;
                        }
                    }
                }

                if (armLeft == false && lookPos.x > gameObject.transform.position.x || armLeft == true && lookPos.x <= gameObject.transform.position.x)
                {

                    lookPos = lookPos - transform.position;

                    float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                    arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    float angle = Mathf.Atan2(lastValidPos.y, lastValidPos.x) * Mathf.Rad2Deg;
                    arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                if (shotTime >= 0 && StatsHolder.characterSelected != 2)
                {

                    shotTime -= Time.deltaTime * 30;

                    roverCanShoot = false;
                    if (StatsHolder.characterSelected != 0)
                    {
                        animator.SetBool("canShoot", false);
                    }
                }
                else
                {
                    roverCanShoot = true;
                    if (StatsHolder.characterSelected != 0)
                    {
                        animator.SetBool("canShoot", true);
                    }
                }
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
                fireWeaponGeneral();
            }
            if (StatsHolder.characterSelected == 1 && special)
            {
                canFly = false;
            }
            else
            {
                canFly = true;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (shooting && StatsHolder.characterSelected == 0 && canPutGunAway)
                {
                    arm.SetActive(false);
                }
                if (canPutGunAway)
                {
                    shooting = false;
                }
            }
            if (GameInputManager.GetKeyDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (GameInputManager.GetKeyUp("Jump"))
            {

                if (velocity.y > 0 && !isFlying && !isBooting)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
            if (GameInputManager.GetKeyDown("Jump") && !grounded)
            {
                if (amtFuel > 0)
                {
                    if (canFly)
                    {
                        isFlying = true;
                    }

                }
                else
                {
                    if (canFly)
                    {
                        isSmoking = true;
                    }
                }
            }
            /*if (Input.GetButtonDown ("Boots")) {
            isBooting = true;
        }
            /*if (Input.GetButtonUp ("Boots")){
            isBooting = false;
        }
            if (GameInputManager.GetKeyUp("Jump"))
            {
                isFlying = false;
                isSmoking = false;
            }
            if (isFlying || isBooting)
            {
                //Debug.Log (amtFuel);

                if (velocity.y < 10 && amtFuel > 0)
                {

                    if (velocity.y < 2)
                    {
                        amtFuel -= Time.deltaTime * 10;
                    }
                    else
                    {
                        amtFuel -= Time.deltaTime * 35;
                    }
                    fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 60.6f);
                    lastAmtFuel = amtFuel;

                    velocity.y += 70f * Time.deltaTime;
                }
                /*  move.x = transform.up.x * 2;
                  velocity.y = transform.up.y * 10;
                  if (right)
                  {
                      gameObject.transform.eulerAngles = new Vector3(0, 0, gameObject.transform.eulerAngles.z - 3f);
                  }
                  if (left)
                  {
                      gameObject.transform.eulerAngles = new Vector3(0, 0, gameObject.transform.eulerAngles.z + 3f);
                  }
                  amtFuel -= Time.deltaTime * 10;
                  fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 40);
                  lastAmtFuel = amtFuel;
            }
            if (amtFuel != lastAmtFuel)
            {
                lastAmtFuel = amtFuel;
                fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 60.6f);
            }
            //flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));


            //  if (flipSprite) 
            //{
            //spriteRenderer.flipX = !spriteRenderer.flipX;
            //}




            if (GameInputManager.GetKey("Focus") && amtFuel < 264)
            {
                focus = true;

                dance = false;

            }

            if (GameInputManager.GetKeyDown("Special") && !dead && amtFuel > 0)
            {

                if (StatsHolder.characterSelected == 1)
                {
                    specialMult = 1.5f;
                }
                if (StatsHolder.characterSelected == 0)
                {
                    if (amtFuel > 99)
                    {
                        if (!special)
                        {
                            amtFuel -= 100;
                            special = true;
                            shotTimeMax = normShotMax / 2;
                            normBurst = true;
                            StartCoroutine(StopBursting());
                            CmdAddPlasmaParticles();
                        }
                    }
                }
                if (StatsHolder.characterSelected == 3)
                {
                    if (amtFuel > 19)
                    {
                        if (!special)
                        {
                            amtFuel -= 20;
                            special = true;
                        }
                    }
                    else
                    {
                        special = false;
                    }
                    //  isInCrouch = true;
                    //  animator.SetBool("crouch", isInCrouch);
                    //  shouldMove = false;
                }
                else if (StatsHolder.characterSelected != 0)
                {
                    special = true;
                }
                if (StatsHolder.characterSelected == 2 && amtFuel > 49 && !owlAttackUsed)
                {
                    amtFuel -= 50;
                    owlAttackUsed = true;

                    birdAnimator.SetBool("hidden", true);
                    animator.SetBool("birdFired", true);
                    Debug.Log("hide");
                    special = false;
                    Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 dir = (lastValidPos - sp).normalized;
                    Vector3 shootPosition = owl.transform.position;
                    /*  if (flipSprite) {
                            shootPosition = bulletSpawn2.position;
                        }
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
            if (GameInputManager.GetKeyUp("Special") || amtFuel < 1)
            {
                if (StatsHolder.characterSelected != 3 && StatsHolder.characterSelected != 0)
                {
                    special = false;
                }
                specialMult = 1;
                /* if (StatsHolder.characterSelected == 0)
                 {
                     shotTimeMax = normShotMa
                 }
            }
            if (GameInputManager.GetKeyDown("Dance") && !focus)
            {
                dance = true;
                startDancingTime = Time.fixedTime;
            }
            if (!grounded || Mathf.Abs(velocity.x) > 0.001 || isFlying || isSmoking || shooting)
            {

                dance = false;
                focus = false;
            }
            if (!focus && previousFocus)
            {
                previousFocus = false;
                focusBar.SetBool("focus", false);
                //focusBarHolder.SetActive (false);
            }
            if (focus && !previousFocus)
            {
                //focusBarHolder.SetActive (true);
                focusBar.SetBool("focus", true);
            }
            previousFocus = focus;
            animator.SetBool("dance", dance);
            if (dance && Time.fixedTime - startDancingTime > 25 && !hasAchieved)
            {
                hasAchieved = true;
                GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(11);
            }
            if (StatsHolder.characterSelected == 2)
            {
                birdAnimator.SetBool("dance", dance);
            }
            animator.SetBool("grounded", grounded);
            animator.SetFloat("velocityX", (Mathf.Abs(velocity.x) / maxSpeed) > 0.25 ? (Mathf.Abs(velocity.x) / maxSpeed) : 0);
            if (StatsHolder.characterSelected == 3 || StatsHolder.characterSelected == 2)
            {
                animator.SetFloat("velocityY", velocity.y / maxSpeed);
            }
            if (StatsHolder.characterSelected == 1 && special)
            {
                isFlying = false;
            }
            animator.SetBool("rocketing", isFlying);

            animator.SetBool("isSmoking", isSmoking);
            if (isFlying)
            {
                if (!jetpackAudio)
                {
                    jetpackAudio = true;
                    //audioData.UnPause ();
                }
            }
            else
            {
                if (jetpackAudio)
                {
                    jetpackAudio = false;
                    //audioData.Pause ();
                }
            }
            if (amtFuel < 264 && focus)
            {
                //animator.SetBool ("rage", special);

                //fuelMeter.rectTransform.sizeDelta = new Vector2 (amtFuel, 40);
                lastAmtFuel = amtFuel;
                animator.SetBool("focus", focus);
            }
            else
            {
                animator.SetBool("focus", false);
            }
            if (amtFuel > 0 && special)
            {
                //animator.SetBool ("rage", special);

                fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 60.6f);
                lastAmtFuel = amtFuel;
                if (StatsHolder.characterSelected == 1)
                {

                    if (Mathf.Abs(velocity.x) > 0.001)
                    {
                        amtFuel -= Time.deltaTime * 35;
                        animator.SetBool("rage", special);
                    }
                }
            }
            else
            {
                if (StatsHolder.characterSelected == 1)
                {
                    animator.SetBool("rage", false);
                }
            }
            if (StatsHolder.characterSelected == 3)
            {
                animator.SetBool("rage", special);

            }
            else
            {
                if (StatsHolder.characterSelected == 3)
                {
                    animator.SetBool("rage", false);
                }
            }
            if (StatsHolder.characterSelected == 0)
            {
                animator.SetBool("shooting", shooting);
            }

            targetVelocity = move * maxSpeed * sprintMultiplier * specialMult;
            //Debug.Log (animator.GetBool(grounded));
        }
    }


    private IEnumerator unhideOwl()
    {
        yield return new WaitForSeconds(3);
        owlAttackUsed = false;
        birdAnimator.SetBool("hidden", false);
        animator.SetBool("birdFired", false);
    }
    /*public void SetName(string _name){
            Debug.Log ("Hook is Working for:");
        Debug.Log ("Server?" + isServer);
        Debug.Log ("Local Player?" + isLocalPlayer);
        if (!isServer && !isLocalPlayer) {
            Debug.Log ("Hook is REALLY Working");
            namePlate.text = _name;
                userNameLocal = _name;
        }
    }
    public void checkForArmTurnOff()
    {
        Debug.Log("File read out: " + canPutGunAway);
        if (canPutGunAway)
        {
            Debug.Log("TURN OFF");
            arm.SetActive(false);
            shooting = false;
        }
    }
    public void subtractFocusFuelEvenly()
    {
        if (amtFuel < 264)
        {
            amtFuel += Time.deltaTime * 15;
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
    public void phaseOut()
    {
        //Debug.Log ("WHY VEN NO FADE");
        //  Debug.Log ("WHY VEN NO FADE");
        //Debug.Log ("WHY VEN NO FADE");
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
        shooting = false;
        isFlying = false;
        special = false;
        isSmoking = false;
        if (StatsHolder.characterSelected == 1 || StatsHolder.characterSelected == 3 || StatsHolder.characterSelected == 2)
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
            shotTimeMax = 102;
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

        SpawnNewStar(pos);


        RpcUpdateFuelBar();
        //fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 40);
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
        AddForceOnAllPelletsServer(pellet1, pellet2, pellet3, pellet4, pellet5, direct, whoShot);



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
        AddForceOnArrowServer(pellet1, direct, whoShot, 1);
        AddForceOnArrowServer(pellet2, direct, whoShot, 2);
        AddForceOnArrowServer(pellet3, direct, whoShot, 3);
        Destroy(pellet1, 1.2f);
        Destroy(pellet2, 1.2f);
        Destroy(pellet3, 1.2f);

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
        AddForceOnAllServer(bullet, direct, whoShot, raged);


        if (!raged)
        {
            Destroy(bullet, 1.0f);
        }
        else
        {
            Destroy(bullet, 1.0f);
        }
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
        particlePrefab.transform.localPosition = new Vector3(0, 1.07f, 0);

    }
    [Command]
    void CmdFireOwl(Vector3 direct, Vector2 pos, string whoShot, bool flipped)
    {
        var bullet = (GameObject)Instantiate(
            owlPrefab,
            pos,
            bulletSpawn.rotation);

        NetworkServer.Spawn(bullet);
        AddForceOnOwlServer(bullet, direct, whoShot, flipped);
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


        Debug.Log("force");
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //Fire (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation, dir, 1000);

        //bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);
        //RpcAddForceOnAllGrenade(bullet, whoShot, neg);
        AddForceOnAllGrenadeServer(bullet, whoShot, neg);



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
        AddForceOnAllFireballServer(bullet, direct, whoShot);



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
        AddForceOnOwlServer(bullet, direct, whoShot, flipped);
        RpcAddForceOnDragon(pos, direct, whoShot, flipped);

        Destroy(bullet, 3.0f);
    }
    [ClientRpc]
    void RpcUpdateFuelBar()
    {
        //Debug.Log ("Proper amount fuel");
        if (isLocalPlayer)
        {

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
            bullet.GetComponent<Rigidbody2D>().AddForce(direct * 600);
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
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);

        var bullet2 = (GameObject)Instantiate(
         localBulletPrefab,
         pos,
         bulletSpawn.rotation);
        // bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 400);
        bullet2.GetComponent<LocalBulletBase>().whoShot = whoFired;
        Destroy(bullet2, 5.0f);
        //Vector3 armZRotation = new Vector3(0, 0, syncZRot);
        //arm.transform.localRotation = arm.transform.localRotation = Quaternion.Euler(armZRotation);
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
            bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 600);
            bullet2.GetComponent<SpriteRenderer>().color = new Color(0.99f, 0.99f, 0.35f);
        }
        else
        {
            bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
            bullet2.GetComponent<SpriteRenderer>().color = new Color(0.486f, 0.76f, 0.96f);
        }

        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;

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
    void AddForceOnOwlServer(GameObject bullet2, Vector3 direct, string whoFired, bool flipped)
    {
        bullet2.GetComponent<Rigidbody2D>().AddForce(direct * 800);
        bullet2.GetComponent<ServerBulletBase>().whoFiredMe = whoFired;
        if (flipped)
        {
            bullet2.GetComponent<SpriteRenderer>().flipY = true;
        }

    }
    [ClientRpc]
    void RpcAddForceOnArrow(Vector2 pos, Vector3 direct, int numArrow, Quaternion rot, string whoShot)
    {
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);

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


        //arrow1.transform.rotation = Quaternion.
        // arrow1.transform.forward = arrow1.GetComponent<Rigidbody2D> ().velocity;
        /*private Vector2 RotateVector(Vector2 v, float angle)
        {
            float _x = v.x*Mathf.Cos(angle) - v.y*Mathf.Sin(angle);
            float _y = v.x*Mathf.Sin(angle) + v.y*Mathf.Cos(angle);
            return new Vector2(_x,_y);  
        } 
        Destroy(arrow1, 1.2f);

        // Vector3 armZRotation = new Vector3(0, 0, syncZRot);
        // arm.transform.localRotation = arm.transform.localRotation = Quaternion.Euler(armZRotation);
        armAnim.SetBool("shoot", true);
    }
    void AddForceOnArrowServer(GameObject arrow1, Vector3 direct, string whoFired, int numArrow)
    {
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);


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

        //arrow1.transform.rotation = Quaternion.
        // arrow1.transform.forward = arrow1.GetComponent<Rigidbody2D> ().velocity;
        /*private Vector2 RotateVector(Vector2 v, float angle)
        {
            float _x = v.x*Mathf.Cos(angle) - v.y*Mathf.Sin(angle);
            float _y = v.x*Mathf.Sin(angle) + v.y*Mathf.Cos(angle);
            return new Vector2(_x,_y);  
        } 
    }
    [ClientRpc]
    void RpcAddForceOnAllPellets(Vector2 pos, Vector3 direct, string whoFired)
    {
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);
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
        //Vector3 armZRotation = new Vector3(0, 0, syncZRot);
        //arm.transform.localRotation = arm.transform.localRotation = Quaternion.Euler(armZRotation);
        /*private Vector2 RotateVector(Vector2 v, float angle)
        {
            float _x = v.x*Mathf.Cos(angle) - v.y*Mathf.Sin(angle);
            float _y = v.x*Mathf.Sin(angle) + v.y*Mathf.Cos(angle);
            return new Vector2(_x,_y);  
        } 
    }
    void AddForceOnAllPelletsServer(GameObject pellet1, GameObject pellet2, GameObject pellet3, GameObject pellet4,
    GameObject pellet5, Vector3 direct, string whoFired)
    {
        //Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 dir = (lastValidPos - sp).normalized;
        //GameObject bullet = ClientScene.FindLocalObject(bullet_id);

        Debug.Log("DIRECT");
        Debug.Log(direct);
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

        /*private Vector2 RotateVector(Vector2 v, float angle)
        {
            float _x = v.x*Mathf.Cos(angle) - v.y*Mathf.Sin(angle);
            float _y = v.x*Mathf.Sin(angle) + v.y*Mathf.Cos(angle);
            return new Vector2(_x,_y);  
        } 
    }
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

        /*  var newPlayer = Instantiate(normanPrefab, spawnPoint, Quaternion.identity) as GameObject;        
          if (which == 1) {
              newPlayer = Instantiate(roverPrefab, spawnPoint, Quaternion.identity) as GameObject;        
          }
          if (which == 2) {
              newPlayer = Instantiate(venusianPrefab, spawnPoint, Quaternion.identity) as GameObject;        
          }*/
       /* if (which == 0)
        {
            NetworkServer.ReplacePlayerForConnection(conn, Instantiate(normanPrefab, spawnPoint, Quaternion.identity) as GameObject, CustomNetworkManager.importantIdMaybe);
        }
        if (which == 1)
        {
            NetworkServer.ReplacePlayerForConnection(conn, Instantiate(roverPrefab, spawnPoint, Quaternion.identity) as GameObject, CustomNetworkManager.importantIdMaybe);
        }
        if (which == 2)
        {
            NetworkServer.ReplacePlayerForConnection(conn, Instantiate(venusianPrefab, spawnPoint, Quaternion.identity) as GameObject, CustomNetworkManager.importantIdMaybe);
        }
        if (which == 3)
        {
            NetworkServer.ReplacePlayerForConnection(conn, Instantiate(juppernautPrefab, spawnPoint, Quaternion.identity) as GameObject, CustomNetworkManager.importantIdMaybe);
        }
        //NetworkServer.ReplacePlayerForConnection(conn, newPlayer, CustomNetworkManager.importantIdMaybe);
        //  Destroy (newPlayer);
        //RpcKillOldPlayer (oldPlayer);  
        //NetworkServer.UnSpawn (oldPlayer);
        //NetworkServer.Destroy (oldPlayer);
    }


    [ClientRpc]
    void RpcKillOldPlayer(GameObject oldPlayer)
    {
        //  Debug.Log ("I CAN RUN THIS AT LEAST");
        //Debug.Log (newPlayer.name);
        /*if (GetName.userName == newPlayer.name) {
                newPlayer.GetComponent<Health> ().resetTimer ();
                Debug.Log ("RESET TIMER PLAYER BA CAW");
            Debug.Log ("RESET TIMER PLAYER BA CAW");
            Debug.Log ("RESET TIMER PLAYER BA CAW");
            Debug.Log ("RESET TIMER PLAYER BA CAW");
        }
        //Destroy (oldPlayer);
        //  NetworkServer.Destroy (oldPlayer);
    }
    [Command]
    void CmdUpdateFireballSprite(int which)
    {
        currentReloadStage = which;

    }

    void UpdateFireballSprite(int oldValue, int newValue)
    {
        if (!isLocalPlayer)
        {
            if (!isServer)
            {
                currentReloadStage = newValue;
            }
            if (roverGun == null)
            {
                roverGun = arm.GetComponent<SpriteRenderer>();
            }
            if (roverGun != null)
            {
                roverGun.sprite = jupGunSprites[5 - newValue];
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
        arm.SetActive(false);
        normBurst = false;
        shotTimeMax = normShotMax;
        special = false;
    }
}
*/