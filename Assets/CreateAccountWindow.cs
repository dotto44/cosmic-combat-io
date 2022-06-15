using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountWindow : MonoBehaviour {
	public InputField displayNameInput, usernameInput, passwordInput;

	
	[SerializeField] private ErrorWindow errorMessage;

	[SerializeField] LoginWindow congratsMessage;

	[SerializeField] InputField displayNameText;

	[SerializeField] Button loginButton;
	[SerializeField] Text loginText;

	[SerializeField] StatsHolder holder;

	[SerializeField] Sprite profilePic0;
	private bool madeAccount = false;
    private void OnDisable()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        displayNameInput.text = "";
    }
    public void createAccount(){
		Debug.Log ("Running Create Method");
		if (displayNameInput.text.Length > 0 && usernameInput.text.Length > 0 && passwordInput.text.Length > 0) {
			new GameSparks.Api.Requests.RegistrationRequest ()
			.SetDisplayName (displayNameInput.text)
			.SetPassword (passwordInput.text)
			.SetUserName (usernameInput.text)
			.Send ((response) => {
				if (!response.HasErrors) {
						madeAccount = true;
					loginButton.image.sprite = profilePic0;
					Debug.Log ("Player Registered");
					//otherWindow.turnOnWindow ();
					gameObject.GetComponent<LoginWindow> ().turnOffWindow ();
					Debug.Log (displayNameInput.text);
					Debug.Log (usernameInput.text);
					Debug.Log (passwordInput.text);
						congratsMessage.turnOnWindow();
						AddPreviousAmt(StatsHolder.stardustAmt);
						loginText.text = "Logged In";
						//loginButton.interactable = false;
						if(displayNameText.text.Length == 0 || displayNameText.text == "Name"){
						displayNameText.text = displayNameInput.text;
						}
				} else {
					Debug.Log ("Error Registering Player");
                    errorMessage.sendErrorMessage(ErrorMessages.whatsMyErrorMessage("REGISTRATION", response.Errors));
                }
			}
			);
		} else {
			if (!madeAccount) {
				errorMessage.sendErrorMessage ("PLEASE MAKE SURE YOU FILLED IN ALL OF THE FIELDS. ");
			}
		}

}
	void AddPreviousAmt (int amount) {
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("Add_Stardust").SetEventAttribute("amount", amount).Send((response) => {
			if (!response.HasErrors) {
				AddBonus();
				Debug.Log("ADDED " + amount + " MOONROCKS");
			} else {
				Debug.Log("Error Saving Player Data...");
			}
		});
	}
	public void AddBonus(){
		StatsHolder.stardustAmt += 100;
		holder.updateStardustText(true);
	}
}