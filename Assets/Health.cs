using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using GameSparks.Core;

public class Health : NetworkBehaviour {
    [SyncVar] private double extraHealth = 0;
	 private int baseHealth = 100;
	[SyncVar(hook = "OnChangeHealth")] double currentHealth = 100;

    [SerializeField] HealthBars healthBars;
	public Image healthBar;

	DeathUI deathUI;

	private GameObject canvas1;
	private GameObject imageHolder;
	private GameObject canvas2;

	private NetworkStartPosition[] spawnPoints;
	public int currentKillCount = 0;
	[SerializeField] GameObject arm;

    private bool eligibleForAward = false;
    private bool comingFromAddHealth = false;
	private float startTime = 0;
	public bool isRover = false;

    private int collectedHearts = 0;
	public int topPlacement = 20;
	public Text killText;

    private bool hasAddedCurrency = false;

    public void Awake()
    {
		baseHealth = Damage.healthValuesByTag[gameObject.tag];
	}
    public override void OnStartLocalPlayer ()
	{
		killText = GameObject.FindGameObjectWithTag("Canvas(2)").GetComponentInChildren<Text>();
		killText.text = "" + currentKillCount;
		canvas1 = GameObject.FindGameObjectWithTag("Feed");
		imageHolder = canvas1.transform.GetChild (0).gameObject;
		spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		startTime = Time.time;
	}
#if UNITY_SERVER || UNITY_EDITOR
	public override void OnStartServer()
	{
		//baseHealth = Damage.healthValuesByTag[gameObject.tag];
		currentHealth = baseHealth;
	}
	public double getExtraHealth()
    {
		return extraHealth;
    }
	public void addExtraHealth(double amt)
	{
		extraHealth += amt;
		ServerAddHealth(amt);
	}
	void ServerAddHealth(double amount)
	{
		currentHealth += amount;
		/* if (currentHealth >= baseHealth)
		 {
            currentHealth = baseHealth;
        }*/
	}
#endif
	public void fallOff(){
        if (!isServer)
        {
            CmdKillPlayer(GetName.userName);
        }
        else
        {
            KillPlayer(GetName.userName);
        }

    }
	public void updatePlacement(int value){
		topPlacement = value;
		//Debug.Log ("top placement = " + value);
	}
	public void AddHealth(double amount){
        if (isLocalPlayer)
        {
            collectedHearts++;
            comingFromAddHealth = true;
        }
       
        if (!isServer && isLocalPlayer)
        {
            CmdAddHealth(amount);
        }
       
    }
    
    public void TakeDamage(double amount, string whosDead, string whoShotGun, string whatMethod)
	{
		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 1)
		{
			currentHealth = baseHealth;
		    leaderBoardUpdate(whosDead, whoShotGun);
			RpcUpdateKills (whoShotGun);
			RpcRespawn(whosDead, whoShotGun, whatMethod);

		}

		//healthBar.rectTransform.sizeDelta = new Vector2((currentHealth/100) * 264, healthBar.rectTransform.sizeDelta.y);
	}
	public void ResetHealth()
	{
       
        if (!isServer)
		{
			return;
		}
		currentHealth = baseHealth;
		if (isRover) {
			RpcTurnArmOn ();
		}
	}

	void OnChangeHealth (double oldLocalHealth, double currentHealthLocal)
	{
		double healthFloat = currentHealthLocal;

		if (!isServer){
            if (currentHealthLocal < currentHealth)
            {
                healthBars.gotHurt();
            }
            currentHealth = currentHealthLocal;
		}
        //Here

        healthBars.setHealthMeter(healthFloat, baseHealth + extraHealth);
      
	    if(isLocalPlayer)
        {
            if (healthFloat >= 100 && eligibleForAward && comingFromAddHealth)
            {
                GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(7);
            }
            if (healthFloat < 15)
            {
                eligibleForAward = true;
            }
            comingFromAddHealth = false;
        }
    }
	public void resetTimer(){
		//Debug.Log ("ResetTimer Not Local");
		if (isLocalPlayer) {
            Debug.Log ("ResetTimer" + eligibleForAward);
            eligibleForAward = false;
            Debug.Log("ResetTimerDone" + eligibleForAward);
            CmdResetPlayerHealth ();
			startTime = Time.time;
			currentKillCount = 0;
            hasAddedCurrency = false;
            Debug.Log ("Kill count set to 0 on this client");
			killText.text = "" + currentKillCount;
			topPlacement = 20;
          
            Vector3 spawnPoint = Vector3.zero;

			// If there is a spawn point array and the array is not empty, pick one at random
			if (spawnPoints != null && spawnPoints.Length > 0) {
				spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			}

			// Set the player’s position to the chosen spawn point

			transform.position = spawnPoint;

			//gameObject.GetComponent<CapsuleCollider2D> ().enabled = true;

		} 


	}
	[ClientRpc]
	void RpcTurnArmOn(){
		//gameObject.GetComponent<PlayerSyncRotation> ().startArmVisibilty ();
	}
	[ClientRpc]
	void RpcRespawn(string whosDead2, string whoShotGun2, string whatMethod)
	{

	
		//gameObject.GetComponent<PlayerSyncRotation> ().endArmVisibilty ();
		if (isLocalPlayer) {
            gameObject.GetComponent<PlayerController>().shouldMove = false;


            gameObject.GetComponent<PlayerController>().refillFuelOnDeath ();
			canvas1 = GameObject.FindGameObjectWithTag("Feed");
			//	imageHolder = canvas1.transform.GetChild (0).gameObject;
			//	imageHolder.GetComponent<Killfeed> ().addKillToFeed (whosDead2, "shot", whoShotGun2);
			// Set the spawn point to origin as a default value
			if (currentKillCount != 0 && !hasAddedCurrency) {
                hasAddedCurrency = true;
				AddCurrency (currentKillCount * 10);
				Debug.Log("ADD " + currentKillCount * 10 + " MOONROCKS");
			}
			pullThisUpOnClient( whoShotGun2, whosDead2, whatMethod);
            //updateDisplayHealthOnNewLife();
            //
        } else {
        if (!isServer)
        {
            killFeedUpdate(whosDead2, whoShotGun2, whatMethod);
        }
        }
	}
	public void pullThisUpOnClient(string whoShotGun2, string whoDied, string whatMethod){

		deathUI = GameObject.FindGameObjectWithTag("DeathUI").GetComponent<DeathUI>();
        //	Animator myAnimator = gameObject.GetComponent<Animator> ();
        //   myAnimator.SetBool ("Dead", true);
        //gameObject.GetComponent<PlayerController> ().freezePlayer ();
        //	Debug.Log(currentKillCount);
        //Debug.Log(GameObject.Find(whoDied).GetComponent<Health>().currentKillCount);
        GameObject myPlayer = GameObject.Find(whoDied);
        PlayerController myPlayerCont = myPlayer.GetComponent<PlayerController>();
      //  int plasmaStars = myPlayer.GetComponent<PlayerController>().collectAndResetStarCount();
        deathUI.startUI(whatMethod, myPlayer.GetComponent<Health>().currentKillCount, startTime, gameObject, topPlacement, myPlayerCont.collectAndResetStarCount(), myPlayerCont.resetAndCollectBasicAttacks(), myPlayerCont.resetAndCollectSpecialAttacks(), collectedHearts);
        collectedHearts = 0;
        gameObject.GetComponent<PlayerController> ().phaseOut ();
	}
	void AddCurrency (int amount) {
		if (GS.Authenticated) {
			new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("Add_Stardust").SetEventAttribute ("amount", amount).Send ((response) => {
				if (!response.HasErrors) {
					Debug.Log ("Kill Stardust Saved To GameSparks...");
				} else {
					Debug.Log ("Error Saving Player Data...");
				}
			});
		}
	}

	public void killFeedUpdate(string whosDead3, string whoShotGun3, string whatMethod){
        Debug.Log("In here");
        if (canvas1 == null)
        {
            canvas1 = GameObject.FindGameObjectWithTag("Feed");
        }
        if (canvas1 != null) {
            Debug.Log("But here?");
            imageHolder = canvas1.transform.GetChild (0).gameObject;

			imageHolder.GetComponent<Killfeed> ().addKillToFeed (whosDead3, "shot", whoShotGun3, whatMethod);
		}
	}

	[ClientRpc]
	 void RpcUpdateKills(string whoKilled){
		Debug.Log (GetName.userName);
		Debug.Log (whoKilled);
		simpleUpdate (whoKilled);
	}
	public void simpleUpdate(string whoKilled2){
		if (GetName.userName == whoKilled2 && isClient) {
			Health hold = GameObject.Find (whoKilled2).GetComponent<Health> ();
			hold.currentKillCount++;

			Debug.Log ("Added One to kill count on this client");
			Debug.Log (currentKillCount);
            checkForAchievement(hold.currentKillCount);
			GameObject koTextHolder = GameObject.FindGameObjectWithTag("Canvas(2)");
            if(koTextHolder != null)
			{
				Text koText = koTextHolder.GetComponentInChildren<Text>();
				if (koText != null)
				{
					koText.text = "" + hold.currentKillCount;
				}
			}
		}
	}
	public void leaderBoardUpdate(string whosDead3, string whoShotGun3){
		NameManager nameManager = GameObject.FindGameObjectWithTag ("NameManager").GetComponent<NameManager> ();
		if (whoShotGun3 != null && whoShotGun3 != "") {
			nameManager.playerList [whoShotGun3]++;
		}
		//nameManager.playerList [whosDead3] = 0; Don't set count to 0 anymore
		canvas2 = GameObject.FindGameObjectWithTag ("LeaderBoard");
		if (canvas2 != null) {
			canvas2.GetComponent<LeaderBoard> ().updateLeaderboard ();
		}
	}

    private void checkForAchievement(int numOfKills)
    {
        if(numOfKills == 3)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(1);
        }
        if (numOfKills == 5)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(2);
        }
        if (numOfKills == 10)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(3);
        }
		if (numOfKills == 20)
		{
			GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(12);
		}
	}
    [Command]
    void CmdSetPlayerKillsTo0(string player)
    {
		Debug.Log("CMD SET PLAYER KILLS TO 0");
		NameManager nameManager = GameObject.FindGameObjectWithTag("NameManager").GetComponent<NameManager>();
		nameManager.playerList[player] = 0;
		canvas2 = GameObject.FindGameObjectWithTag("LeaderBoard");
		if (canvas2 != null)
		{
			canvas2.GetComponent<LeaderBoard>().updateLeaderboard();
		}
	}
    public void DealSelfDamage(double amount, string whoWhoDead, string method)
    {
		CmdDealSelfDamage(amount, whoWhoDead, method);
	}
	[Command]
	void CmdDealSelfDamage(double amount, string whoWhoDead, string method)
	{
		TakeDamage(amount, whoWhoDead, null, method);
	}
	[Command]
	void CmdKillPlayer(string whoWhoDead){
		TakeDamage(100, whoWhoDead,null, "SPACE");
	}
    void KillPlayer(string whoWhoDead)
    {
        TakeDamage(100, whoWhoDead, null, "SPACE");
    }
    [Command]
	void CmdResetPlayerHealth(){
		ResetHealth ();

	}
	[Command]
	void CmdAddHealth(double amount)
	{
		currentHealth += amount;
		if (currentHealth >= baseHealth)
		{
			currentHealth = baseHealth;
		}
	}
    
    /*  void updateDisplayHealthOnNewLife()
    {
        pastDisplayHealth = 264;
        displayHealth = 264;
    }
  void updateDisplayHealth(float healthFloat)
    {
      
        displayHealth = (healthFloat / 100) * 264;
        if (Mathf.Abs(displayHealth - pastDisplayHealth) > 0.05)
        {
            pastDisplayHealth = healthBar.rectTransform.sizeDelta.x;
        }
        timeExpired = 0;
    }
    void slideHealth()
    {
        if(timeExpired >= 1)
        {
            timeExpired = 1;
        }
        timeExpired += Time.deltaTime * 12;
        healthBar.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(pastDisplayHealth, displayHealth, timeExpired), healthBar.rectTransform.sizeDelta.y);
    }*/
}
