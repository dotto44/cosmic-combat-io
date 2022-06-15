using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class DeathUI : MonoBehaviour {
	public Text title;
    private int numStarsCollected;
    [SerializeField] TMP_Text killStatText;
    [SerializeField] TMP_Text groundText;
    [SerializeField] TMP_Text specialText;
    [SerializeField] TMP_Text normalAttackText;
    [SerializeField] TMP_Text heartText;
    [SerializeField] TMP_Text plasmaStarText;
    public Text placement;
	public Text stardustText;
    [SerializeField] TMP_Text[] killRecords;
	private string timeTextPrivate = "";
    [SerializeField] GameObject killCount;
	public GameObject holder;
    [SerializeField] Animator holderAnim;
	public GameObject holder2;

	public GameObject upperRight;
	public GameObject lowerRight;

	private GameObject player;

	public int pastCharacter;
	private GameObject leaderBoard;
	[SerializeField] Sprite zappedText;
	[SerializeField] Sprite gotLostText;
	[SerializeField] TMP_Text killedHow;
    [SerializeField] CaveScript[] covers;

    [SerializeField] GameObject tip;

    [SerializeField] Text totalStarDUstCount;

    [SerializeField] Image blackBlackBlack;
    [SerializeField] Image goBtn;
    [SerializeField] Text goBtnText;
    [SerializeField] Image statBtn;
    [SerializeField] Image xBtn;
    [SerializeField] Image charBtn;
    [SerializeField] TMP_Text description;
    [SerializeField] DeathUIBar bar;
    private bool isShowingCharMenu;
    private bool isShowingMenu;
    private bool isShowingStatMenu;
    [SerializeField] GameObject statMenu;
    [SerializeField] GameObject charMenu;

    [SerializeField] CharacterLevelDisplay charLevelDisplay;
    public Camera main;
	void OnEnable(){
		pastCharacter = StatsHolder.characterSelected;
	}
    public void pressedStatMenu()
    {
        turnOnMenu(1);
    }
    public void pressedCharMenu()
    {
        turnOnMenu(0);
    }
    private void turnOnMenu(int from)
    {
       if(!isShowingCharMenu && !isShowingStatMenu)
        {
            if(from == 0)
            {
                isShowingCharMenu = true;
                charMenu.SetActive(true);
                statMenu.SetActive(false);
            }
            if (from == 1)
            {
                isShowingStatMenu = true;
                statMenu.SetActive(true);
                charMenu.SetActive(false);
            }
            bar.setVisibleAndHidden();
            return;
        }
        if (isShowingCharMenu)
        {
            if(from == 0)
            {
                isShowingCharMenu = false;
                bar.setVisibleAndHidden();
            }
            else
            {
                isShowingCharMenu = false;
                bar.quickToggle(1);
                isShowingStatMenu = true;
            }
            return;
        }
        if (isShowingStatMenu)
        {
            if (from == 0)
            {
                isShowingCharMenu = true;
                isShowingStatMenu = false;
                bar.quickToggle(0);
            }
            else
            {
                isShowingStatMenu = false;
                bar.setVisibleAndHidden();
            }
            return;
        }

    }
    public void startUI(string how, int kills, float timeAlive, GameObject player1, int topPlacement, int numStarsCollected, int numBasicAttacks, int numSpecialAttcks, int collectedHearts){
		if (leaderBoard == null) {
			leaderBoard = GameObject.FindGameObjectWithTag ("LeaderBoard");
		}
        // main.GetComponent<CinemachineBrain>().enabled = false;

        Debug.Log("STARS COLLECTED: " + numStarsCollected);
        Debug.Log("NUMBER OF BASIC ATTACKS: " + numBasicAttacks);
        plasmaStarText.text = "" + numStarsCollected + " PLASMA PICKUP";
        this.numStarsCollected = numStarsCollected;
        normalAttackText.text = "" + numBasicAttacks + " NORMAL ATTACK";
        specialText.text = "" + numSpecialAttcks + " SPECIAL ATTACK";
        heartText.text = "" + collectedHearts + " HEART PICKUP";
        // plasmaStarText.text = "" + numStarsCollected + " PLASMA STARS";
        isShowingCharMenu = false;
           player = player1;
        holder.SetActive (true);
        holderAnim.CrossFade("FadeInDeathUI", 0.0f);
		//holder2.SetActive (true);
		upperRight.SetActive (false);
        //lowerRight.SetActive (false);
        turnOnHiddenAreaCovers();
        killCount.SetActive(false);
        leaderBoard.SetActive (false);
        Debug.Log(":HOW: " + how);
        killedHow.text = "YOU WERE " + Killfeed.getKillWord(how, true, false);
		float guiTime = Time.time - timeAlive;

		float minutes = guiTime / 60;
		float seconds = guiTime % 60;
		//float fraction = (guiTime * 100) % 100;

		timeTextPrivate = string.Format ("{0:00}:{1:00}", minutes, seconds);
        groundText.text = timeTextPrivate + " TIME ALIVE";

        int tempValue = player1.GetComponent<Health> ().currentKillCount;
       // RecordHolder.addXPtoCharacter(StatsHolder.characterSelected, tempValue * 7);

		//killText.text =  tempValue.ToString();
        description.text = "YOU GOT <sprite=0>" + tempValue * 10 + " MOONROCKS FROM <sprite=1>" + tempValue + " KNOCK OUTS";
        killStatText.text = "" + tempValue + " KNOCK OUT";
        RecordHolder.setKillRecord(StatsHolder.characterSelected, tempValue);
        charLevelDisplay.updateLevelText();
        for (int i = 0; i < killRecords.Length; i++)
        {
            killRecords[i].text = "<sprite=1> " + RecordHolder.getKillsRecord(i);
        }
        if (tempValue != 1)
        {
            killStatText.text += "S";
        }
        if (collectedHearts != 1)
        {
            heartText.text += "S";
        }
        if(numSpecialAttcks != 1)
        {
            specialText.text += "S";
        }
        if (numBasicAttacks != 1)
        {
            normalAttackText.text += "S";
        }
        if (numStarsCollected != 1)
        {
            plasmaStarText.text += "S";
        }
        // stardustText.text = "+" +normalAttackText.text = "" + numBasicAttacks + " NORMAL ATTACKS"; (tempValue * 10).ToString();
        bool candidate500 = false;
        bool candidate1000 = false;
        bool candidate5000 = false;
        if (StatsHolder.stardustAmt < 500)
        {
            candidate500 = true;
        }
        if (StatsHolder.stardustAmt < 1000)
        {
            candidate1000 = true;
        }
        if (StatsHolder.stardustAmt < 5000)
        {
            candidate5000 = true;
        }
        StatsHolder.stardustAmt += tempValue * 10;
        if (StatsHolder.stardustAmt >= 500 && candidate500)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(8);
        }
        if (StatsHolder.stardustAmt >= 1000 && candidate1000)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(9);
        }
        if (StatsHolder.stardustAmt >= 5000 && candidate5000)
        {
            GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(10);
        }
       
		totalStarDUstCount.text = StatsHolder.stardustAmt.ToString("D4");
		//timeText.text = timeTextPrivate;
		placement.text = "" + topPlacement;
		if (topPlacement == 1) {
			placement.text += "st";
		} else if (topPlacement == 2) {
			placement.text += "nd";
		} else if (topPlacement == 3) {
			placement.text += "rd";
		} else if (topPlacement >= 4) {
			placement.text += "th";
		}
		tip.GetComponent<TipText>().newTip ();
	}

	public void respawnPlayer(){
      //  main.GetComponent<CinemachineBrain>().enabled = true;
        //	Debug.Log ("RESPAWNING");
        PlayerController playerScript = player.GetComponent<PlayerController> ();
	//	Debug.Log (playerScript);
		upperRight.SetActive (true);
		lowerRight.SetActive (true);
        killCount.SetActive(true);
        blackBlackBlack.color = new Color(0, 0, 0, 0);
    goBtn.color = new Color(1, 1, 1, 0);
        goBtnText.color = new Color(1,1,1,0);
        statBtn.color = new Color(1, 1, 1, 0);
        xBtn.color = new Color(1, 1, 1, 0);
        charBtn.color = new Color(1, 1, 1, 0);
        killedHow.color = new Color(1, 1, 1, 0);
        description.color = new Color(1, 1, 1, 0);
       // bar.transform.localPosition = new Vector3(0, -75, 0);
        holder.SetActive (false);
        //holder2.SetActive (false);
        turnOffHiddenAreaCovers();
        leaderBoard.SetActive (true);
        if (pastCharacter != StatsHolder.characterSelected) {
            GetName.userNameCanChange = false;

         playerScript.ServerNewSpawn (playerScript, StatsHolder.characterSelected);
			//Debug.Log ("METHOD 1");
		} else {
		//	Debug.Log ("METHOD 2");
		player.SetActive (true);
            playerScript.shouldMove = true;

        player.GetComponent<Health> ().resetTimer ();
		playerScript.phaseIn ();
		}

	}
    private void turnOnHiddenAreaCovers()
    {
            if (covers == null) return;
            for(int i = 0; i < covers.Length; i++)
            {
                Debug.Log("IN COVERS:" + covers.Length);
                covers[i].turnOnInCase();
                covers[i].isInOverride = true;
            }
    }
    private void turnOffHiddenAreaCovers()
    {
        if (covers == null) return;
        for (int i = 0; i < covers.Length; i++)
        {
            covers[i].resetInCase();
            covers[i].isInOverride = false;
        }
    }
}
