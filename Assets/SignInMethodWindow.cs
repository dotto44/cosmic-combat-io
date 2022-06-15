using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using Facebook.Unity;
using GameSparks.Api.Responses;

public class SignInMethodWindow : MonoBehaviour {

	public InputField usernameInput, passwordInput;

    [SerializeField] private ErrorWindow errorMessage;

    [SerializeField] LoginButton loginButtonScript;
	[SerializeField] Image profilePicBig;
	[SerializeField] Text ProfileName;

	[SerializeField] InputField displayName;

	[SerializeField] Button loginButton;
	[SerializeField] Text loginText;

	[SerializeField] StatsHolder holder;

	[SerializeField] InputField deaultNameChanger;
	[SerializeField] Text placeholderDefault;

	[SerializeField] Text accountMessage;

	[SerializeField] Text killCountProfile;


	[SerializeField] LoginWindow congratsMessage;
    private void OnDisable()
    {
        usernameInput.text = "";
        passwordInput.text = "";
    }
    public void logOutFB(){
		if (FB.IsLoggedIn) {
			FB.LogOut ();
		}
		GS.Reset ();
        accountMessage.text = "MAKE AN ACCOUNT TO SAVE STARDUST AND SKINS!";

    }

	public void ConnectWithFacebook()
	{
		if(!FB.IsInitialized)
		{
			Debug.Log("Initializing Facebook");
			FB.Init(FacebookLogin);
		}
		else
		{
			FacebookLogin();
		}
	}


	/// <summary>
	/// When Facebook is ready , this will connect the pleyer to Facebook
	/// After the Player is authenticated it will  call the GS connect
	/// </summary>
	void FacebookLogin()
	{
		if(!FB.IsLoggedIn)
		{
			Debug.Log("Logging into Facebook");
			FB.LogInWithReadPermissions(
				new List<string>() { "public_profile", "email", "user_friends" },
				GameSparksFBConnect
			);
		}
	}
	void TwitterLogin()
	{
		if(!FB.IsLoggedIn)
		{
			Debug.Log("Logging into Twitter");
			FB.LogInWithReadPermissions(
				new List<string>() { "public_profile"},
				GameSparksFBConnect
			);
		}
	}
	void GameSparksFBConnect(ILoginResult result)
	{

		if(FB.IsLoggedIn)
		{
			Debug.Log("Logging into gamesparks with facebook details");
			GSFacebookLogin(AfterFBLogin);
		}
		else
		{
			Debug.Log("Something wrong  with FB");
		}
	}

	//this is the callback that happens when gamesparks has been connected with FB
	private void AfterFBLogin(GameSparks.Api.Responses.AuthenticationResponse _resp)
	{
		Debug.Log(_resp.DisplayName );
	}

	//delegate for asynchronous callbacks
	public delegate void FacebookLoginCallback(AuthenticationResponse _resp);

	void AddPreviousAmt (int amount) {
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("Add_Stardust").SetEventAttribute("amount", amount).Send((response) => {
			if (!response.HasErrors) {
				AddBonus();
				Debug.Log("Kill Stardust Saved To GameSparks...");
			} else {
				Debug.Log("Error Saving Player Data...");
			}
		});
	}
	public void AddBonus(){
		StatsHolder.stardustAmt += 100;
		holder.updateStardustText(true);
	}
	//This method will connect GS with FB
	public void GSFacebookLogin(FacebookLoginCallback _fbLoginCallback )
	{
		Debug.Log("");

		new GameSparks.Api.Requests.FacebookConnectRequest()
			.SetAccessToken(AccessToken.CurrentAccessToken.TokenString)
			.Send((response) => {
				if(!response.HasErrors)
				{
					
					Debug.Log("Logged into gamesparks with facebook");
					_fbLoginCallback(response);
					if(response.NewPlayer == true){
						congratsMessage.turnOnWindow();
						AddPreviousAmt(StatsHolder.stardustAmt);
					}

                    loginButtonScript.UpdateProfilePic();
					gameObject.GetComponent<LoginWindow>().turnOffWindow();
					Debug.Log(displayName.text.Length);
					loginText.text = "Logged In";
				//	loginButton.interactable = false;
					deaultNameChanger.interactable = true;
					deaultNameChanger.text = response.DisplayName;
					placeholderDefault.text = response.DisplayName;
					UpdateCurrency();
					accountMessage.text = response.DisplayName;
					if(displayName.text.Length == 0 || displayName.text == "Name"){
						Debug.Log("UPDATE DISPLAY");
						Debug.Log(response.DisplayName);
						displayName.text = response.DisplayName;
					}
				}
				else
				{
					Debug.Log("Error Logging into facebook");
				}

			});
	}

	public void authenticateLocalPlayer(){
		new GameSparks.Api.Requests.AuthenticationRequest().SetUserName(usernameInput.text).SetPassword(passwordInput.text).Send((response) => {
			if (!response.HasErrors) {
                loginButtonScript.UpdateProfilePic();
				Debug.Log(displayName.text.Length);
				Debug.Log("Player Authenticated..." + "\n Display Name:" + response.DisplayName);
				gameObject.transform.parent.gameObject.GetComponent<WindowManager>().turnOff();
				Debug.Log(displayName.text.Length);
				loginText.text = "Logged In";
				//loginButton.interactable = false;
				deaultNameChanger.interactable = true;
				deaultNameChanger.text = response.DisplayName;
				placeholderDefault.text = response.DisplayName;
				UpdateCurrency();
				accountMessage.text = response.DisplayName;
				if(displayName.text.Length == 0 || displayName.text == "Name"){
					Debug.Log("UPDATE DISPLAY");
					Debug.Log(response.DisplayName);
					displayName.text = response.DisplayName;
				}
			} else {
				Debug.Log("Error Authenticating Player..." + response.Errors.JSON.ToString());
                errorMessage.sendErrorMessage(ErrorMessages.whatsMyErrorMessage("AUTH", response.Errors));

            }
		});
	}
	public void editDefaultName(){
		string dataType = deaultNameChanger.text;
		new  GameSparks.Api.Requests.ChangeUserDetailsRequest()
			.SetDisplayName(dataType)
			.Send((response) => {
				GSData scriptData = response.ScriptData; 
				if (!response.HasErrors) {
					ProfileName.text = dataType.ToUpper();
					placeholderDefault.text = dataType;
				}
			});
		
	}
	public void UpdateCurrency(){
		new GameSparks.Api.Requests.AccountDetailsRequest().Send((response) => {
			if (!response.HasErrors) {
				GSData currencies = response.Currencies; 
				int? stardustValue = currencies.GetInt("STARDUST");
				StatsHolder.stardustAmt = (int)stardustValue;
				GSData virtualGoods = response.VirtualGoods;
               
                Debug.Log(virtualGoods.BaseData);
                ProfileName.text = response.DisplayName.ToUpper();
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
				if(virtualGoods.ContainsKey("SANTA")){
					holder.unlockCostumeNorm(0);
				}
				if(virtualGoods.ContainsKey("TUXEDO")){
					holder.unlockCostumeRover(0);
				}
                if (virtualGoods.ContainsKey("RUST"))
                {
                    holder.unlockCostumeRover(1);
                }
                if (virtualGoods.ContainsKey("ARMOR")){
					holder.unlockCostumeVen(0);
				}
				if(virtualGoods.ContainsKey("ROVER")){
					Debug.Log("Has rover key");
					Debug.Log(virtualGoods.BaseData["ROVER"]);
				
						holder.unlockChar(1);

				}
				if(virtualGoods.ContainsKey("VENUSIAN")){

						holder.unlockChar(2);

				}
				if(virtualGoods.ContainsKey("MERCUTIAN")){
					
						
						holder.unlockChar(3);

				}
				if(virtualGoods.ContainsKey("JUPPERNAUT")){

						holder.unlockChar(4);

				}

                holder.updateStardustText(false);
			}else{
				Debug.Log("Error Accessing Currency");
			}
		});
	}
}
