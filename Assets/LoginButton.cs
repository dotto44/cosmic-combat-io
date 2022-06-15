using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameSparks.Core;
using Facebook.Unity;
public class LoginButton : MonoBehaviour {

    private static readonly string[] shortCodes = { "norm", "rover", "ven", "jup", "nep" };
    public InputField usernameInput, passwordInput;

    [SerializeField] ErrorWindow errorMessage;
    [SerializeField] Text ProfileName;

    [SerializeField] Sprite[] profilePics;
    [SerializeField] Sprite[] profilePicHexes;

   
	[SerializeField] Sprite unloggedAccount;


	[SerializeField] InputField displayName;

	[SerializeField] Button loginButton;
	[SerializeField] Text loginText;

	[SerializeField] StatsHolder holder;

	[SerializeField] InputField deaultNameChanger;
	[SerializeField] Text placeholderDefault;

	[SerializeField] Text accountMessage;

	[SerializeField] LoginWindow wheree;
	[SerializeField] LoginWindow profileSelector;

	[SerializeField] Image profilePicBig;
	[SerializeField] Text killCountProfile;

    [SerializeField] GameObject settingsMenu;
	public bool hasSet = false;
	//private int profilePic = 0;
	public void openANewWindow(){
		if (!GS.Authenticated) {
			wheree.turnOnWindow ();
		} else {
			profileSelector.turnOnWindow ();

		}
	}
	void Start(){
		
	}
	void Update(){
		if (GS.Authenticated && !hasSet) {
			hasSet = true;
			UpdateCurrency ();
			UpdateProfilePic ();
		}
		if (!GS.Authenticated && hasSet) {
			loginButton.image.sprite = unloggedAccount;
			hasSet = false;
			deaultNameChanger.interactable = false;
			holder.resetAllShopComponents ();
		}
	}
    public void updateProfilePic(int pic)
    {
        loginButton.image.sprite = profilePics[pic];
        profilePicBig.sprite = profilePicHexes[pic];
        saveIcon(pic);
    }
  
	public void saveIcon(int whichPic){
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SAVE_PLAYER").SetEventAttribute("PIC", whichPic).Send((response) => {
			if (!response.HasErrors) {
				//Debug.Log("Player Saved To GameSparks...");
			} else {
				//Debug.Log("Error Saving Player Data...");
			}
		});
	}
    public void updateCharPrefData(GSData charData)
    {
        holder.changeCharacterSelected((int)charData.GetNumber("selected"));
        StatsHolder.selectedSkinPerChar[0] = (int)charData.GetNumber("normSkin");
        StatsHolder.selectedSkinPerChar[1] = (int)charData.GetNumber("roverSkin");
        StatsHolder.selectedSkinPerChar[2] = (int)charData.GetNumber("venusianSkin");
        if (charData.ContainsKey("juppernautSkin"))
        {
            StatsHolder.selectedSkinPerChar[3] = (int)charData.GetNumber("juppernautSkin");
        }
        StatsHolder.setPastSelectedSkinsSame();
        holder.updateFromSaveData();
    }

        public void updateKeyBinds(GSData keybindData)
    {
        KeyCode? difFire = (KeyCode?)keybindData.GetNumber("fire1");
        if(difFire != null)
        {
            GameInputManager.swapKey((KeyCode)difFire, "Fire1", false);
        }
        KeyCode? difJump = (KeyCode?)keybindData.GetNumber("jump");
        if (difJump != null)
        {
            GameInputManager.swapKey((KeyCode)difJump, "Jump", false);
        }
        KeyCode? difSpecial = (KeyCode?)keybindData.GetNumber("special");
        if (difSpecial != null)
        {
            GameInputManager.swapKey((KeyCode)difSpecial, "Special", false);
        }
        KeyCode? difDrop = (KeyCode?)keybindData.GetNumber("drop");
        if (difDrop != null)
        {
            GameInputManager.swapKey((KeyCode)difDrop, "Drop", false);
        }
        KeyCode? difLeft = (KeyCode?)keybindData.GetNumber("left");
        if (difLeft != null)
        {
            GameInputManager.swapKey((KeyCode)difLeft, "Left", false);
        }
        KeyCode? difRight = (KeyCode?)keybindData.GetNumber("right");
        if (difRight != null)
        {
            GameInputManager.swapKey((KeyCode)difRight, "Right", false);
        }
        KeyCode? difDance = (KeyCode?)keybindData.GetNumber("dance");
        if (difDance != null)
        {
            GameInputManager.swapKey((KeyCode)difDance, "Dance", false);
        }
        KeyCode? difMap = (KeyCode?)keybindData.GetNumber("map");
        if (difMap != null)
        {
            GameInputManager.swapKey((KeyCode)difMap, "Map", false);
        }
        KeyCode? difFocus = (KeyCode?)keybindData.GetNumber("focus");
        if (difFocus != null)
        {
            GameInputManager.swapKey((KeyCode)difFocus, "Focus", false);
        }
        float? voumeValue = (float?)keybindData.GetNumber("volume");
        if (voumeValue != null)
        {
            StatsHolder.audioValue =(float)voumeValue;
            GameInputManager.localAudioValue = (float)voumeValue;
        }
       if (settingsMenu.activeSelf)
        {
           settingsMenu.GetComponent<KeybindMenu>().updateAllKeyBinders();
        }
    }
    public void updatePRS(GSData recordData)
    {

        for (int i = 0; i < recordData.BaseData.Count; i++)
        {
            if (recordData.GetNumber(shortCodes[i]) != null)
            {
                RecordHolder.setKillRecord(i, (int)recordData.GetNumber(shortCodes[i]));
            }
        }
    }
    public void updateLevels(GSData recordData)
    {
        for (int i = 0; i < StatsHolder.numberOfCharacters; i++)
        {
            RecordHolder.setKillRecord(i, (int)recordData.GetNumber(shortCodes[i]));
        }
    }
    public void UpdateProfilePic(){
		//REMEBR TO ADD THIS TO FB LOGIN TOO
	
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LOAD_PLAYER").Send((response) => {
            Debug.Log("loaded" + response.ScriptData.GetGSData("key_Binds"));
			if (!response.HasErrors) {
                if (response.ScriptData.GetGSData("personal_Records") != null)
                {
                    Debug.Log("GOT PRS");
                    GSData recordData = response.ScriptData.GetGSData("personal_Records");
                    updatePRS(recordData);
                }
                else
                {
                    Debug.Log("NO PRS");
                }
                if (response.ScriptData.GetGSData("level_Data") != null)
                {
                    Debug.Log("GOT LEVELS");
                    GSData recordData = response.ScriptData.GetGSData("level_Data");
                   // updateLevels(recordData);
                }
                else
                {
                    Debug.Log("NO LEVELS");
                }
                if (response.ScriptData.GetGSData("char_Prefs") != null)
                {
                    Debug.Log("GOT CHAR PREFS");
                    GSData charData = response.ScriptData.GetGSData("char_Prefs");
                    updateCharPrefData(charData);
                }
                else
                {
                    Debug.Log("NO CHAR PREFS");
                }
                if (response.ScriptData.GetGSData("key_Binds") != null)
                {
                    Debug.Log("has keybinds");
                    GSData keybindData = response.ScriptData.GetGSData("key_Binds");
                    updateKeyBinds(keybindData);
                }
                if (response.ScriptData.GetGSData("player_Data") != null){
                    Debug.Log("has profile pic");
                    GSData data = response.ScriptData.GetGSData("player_Data");
               
                    long? profilePic = data.GetNumber("profilePic");
					 if(profilePic == 0 || profilePic == null){
						loginButton.image.sprite = profilePics[0];
						profilePicBig.sprite = profilePicHexes[0];
                    }
                    else
                    {
                        loginButton.image.sprite = profilePics[(int)profilePic];
                        profilePicBig.sprite = profilePicHexes[(int)profilePic];
                    }
                 
				}else{
					loginButton.image.sprite = profilePics[0];
					profilePicBig.sprite = profilePicHexes[0];
				}



			} else {
              
            }
		});
	}
	public void UpdateCurrency(){
		new GameSparks.Api.Requests.AccountDetailsRequest ().Send ((response) => {
			if (!response.HasErrors) {
				deaultNameChanger.interactable = true;
				deaultNameChanger.text = response.DisplayName;
				placeholderDefault.text = response.DisplayName;
				//loginButton.interactable = false;
				ProfileName.text = response.DisplayName.ToUpper();
				accountMessage.text = response.DisplayName;
				GSData currencies = response.Currencies; 
				int? stardustValue = currencies.GetInt("STARDUST");
				StatsHolder.stardustAmt = (int)stardustValue;
				GSData virtualGoods = response.VirtualGoods;
                List<string> achievementsList = response.Achievements;
                if (achievementsList != null)
                {
                    int count = 0;
                    if (achievementsList.Contains("OOPSIE"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_OOPSIE"))
                        {
                            holder.setAchievement(0);
                        }
                        else
                        {
                            holder.setAchievementForCollect(0);
                        }
                    }
                    if (achievementsList.Contains("DECENTLY_DANGEROUS"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_DECENTLY_DANGEROUS"))
                        {
                            holder.setAchievement(1);
                        }
                        else
                        {
                            holder.setAchievementForCollect(1);
                        }
                    }
                    if (achievementsList.Contains("SUPERNOVA"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_SUPERNOVA"))
                        {
                            holder.setAchievement(12);
                        }
                        else
                        {
                            holder.setAchievementForCollect(12);
                        }
                    }
                    if (achievementsList.Contains("PRETTY_POWERFUL"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_PRETTY_POWERFUL"))
                        {
                            holder.setAchievement(2);
                        }
                        else
                        {
                            holder.setAchievementForCollect(2);
                        }
                    }
                    if (achievementsList.Contains("GALACTIC_GLORY"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_GALACTIC_GLORY"))
                        {
                            holder.setAchievement(3);
                        }
                        else
                        {
                            holder.setAchievementForCollect(3);
                        }
                    }
                    if (achievementsList.Contains("THE_GAS_GIANT"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_THE_GAS_GIANT"))
                        {
                            holder.setAchievement(4);
                        }
                        else
                        {
                            holder.setAchievementForCollect(4);
                        }
                    }
                    if (achievementsList.Contains("STYLISH"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_STYLISH"))
                        {
                            holder.setAchievement(5);
                        }
                        else
                        {
                            holder.setAchievementForCollect(5);
                        }
                    }
                    if (achievementsList.Contains("AMATEUR_COLLECTION"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_AMATEUR_COLLECTION"))
                        {
                            holder.setAchievement(6);
                        }
                        else
                        {
                            holder.setAchievementForCollect(6);
                        }
                    }
                    if (achievementsList.Contains("PHEW_CLOSE_ONE"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_PHEW_CLOSE_ONE"))
                        {
                            holder.setAchievement(7);
                        }
                        else
                        {
                            holder.setAchievementForCollect(7);
                        }
                    }
                    if (achievementsList.Contains("SAVINGS_ACCOUNT"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_SAVINGS_ACCOUNT"))
                        {
                            holder.setAchievement(8);
                        }
                        else
                        {
                            holder.setAchievementForCollect(8);
                        }
                    }
                    if (achievementsList.Contains("PENNY_PINCHER"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_PENNY_PINCHER"))
                        {
                            holder.setAchievement(9);
                        }
                        else
                        {
                            holder.setAchievementForCollect(9);
                        }
                    }
                    if (achievementsList.Contains("SCROOGE"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_SCROOGE"))
                        {
                            holder.setAchievement(10);
                        }
                        else
                        {
                            holder.setAchievementForCollect(10);
                        }
                    }
                    if (achievementsList.Contains("BOOGIE_FEVER"))
                    {
                        count++;
                        if (achievementsList.Contains("COLLECT_BOOGIE_FEVER"))
                        {
                            holder.setAchievement(11);
                        }
                        else
                        {
                            holder.setAchievementForCollect(11);
                        }
                    }
                    holder.setAchievementNum(count);
                }
                else
                {
                    holder.setAchievementNum(0);
                }

                if (virtualGoods.ContainsKey("GOLDEN")){
					holder.unlockCostumeNorm(2);
				}
                if (virtualGoods.ContainsKey("COOLGUY"))
                {
                    holder.unlockCostumeNorm(1);
                }
                if (virtualGoods.ContainsKey("PIRATE"))
                {
                    holder.unlockCostumeJupper(2);
                }
                if (virtualGoods.ContainsKey("SANTA")){
					holder.unlockCostumeNorm(0);
				}
				if(virtualGoods.ContainsKey("TUXEDO")){
					holder.unlockCostumeRover(0);
				}
                if (virtualGoods.ContainsKey("ARMOR"))
                {
                    holder.unlockCostumeVen(0);
                }
                if (virtualGoods.ContainsKey("RUST"))
                {
                    holder.unlockCostumeRover(1);
                }

				if(virtualGoods.ContainsKey("ROVER")){
					

					holder.unlockChar(1);

				}
				if(virtualGoods.ContainsKey("VENUSIAN")){

					holder.unlockChar(2);

				}
			
				if(virtualGoods.ContainsKey("JUPPERNAUT")){

					holder.unlockChar(3);

				}
                if (virtualGoods.ContainsKey("NEPTUNIAN"))
                {

                    holder.unlockChar(4);

                }
                if (virtualGoods.ContainsKey("PLUTONIAN"))
                {

                    holder.unlockChar(5);

                }
                Debug.Log(virtualGoods.BaseData.Keys.ToString());
                holder.updateStardustText(false);

			}else{
				hasSet = false;
			}
		});
			}
}
