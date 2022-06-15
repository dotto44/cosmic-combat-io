using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mirror;

public class LeaderBoard : NetworkBehaviour {
	private string oldText;
	private string newText;
	private int numLines = 0;
	private NameManager nameManager; 
	public Text nameText; 
	public Text intText; 

	private string player1;
	private int killCount1;
	private string player2;
	private int killCount2;
	private string player3;
	private int killCount3;
	private string player4;
	private int killCount4;


	private string leaderboardNames;
	private string leaderboardKills;


	void Start(){
		gameObject.GetComponent<Canvas> ().worldCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}
	public void updateLeaderboard(){
        //THIS SHOULD BE CALLED ON SERVER
		if (nameManager == null) {
			nameManager = GameObject.FindGameObjectWithTag ("NameManager").GetComponent<NameManager> ();
		}
		var sortedDict = from entry in nameManager.playerList orderby entry.Value descending
			                 select entry;
			int count = nameManager.playerList.Count;
			int index = 0;
			intText.text = "";
			nameText.text = "";
           
            if(sortedDict.FirstOrDefault().Value >= 20)
            {
			    GameObject.Find("NextGame").GetComponent<NextGame>().gameWon(sortedDict.First().Key);
            }
			foreach (KeyValuePair<string, int> value in sortedDict) {
				if (index < count && index < 3) {
					nameText.text += ((index + 1).ToString () + "." + value.Key);
					nameText.text += "\n\n\n";
					intText.text += value.Value.ToString ();
					intText.text += "\n\n\n";
				}

				index++;
			/*	if (GetName.userName == value.Key.ToString ()) {
					placement.text = "" + (index);
					if (index < healthScript.topPlacement) {
						healthScript.updatePlacement (index);
					}
					killCount.text = value.Value.ToString ();
					if (index == 1) {
						placement.text += "st";
					} else if (index == 2) {
						placement.text += "nd";
					} else if (index == 3) {
						placement.text += "rd";
					} else if (index >= 4) {
						placement.text += "th";
					}
				}*/
			}
		string intergerText = intText.text;
		string nameTextt = nameText.text;

        if (!isServer)
        {
            CmdUpdateClients(nameTextt, intergerText);
        }
        else
        {
            RpcUpdateClients(nameTextt, intergerText);
        }
    }




	//
	public void updateLeaderboardFirst(){
		if (nameManager == null) {
			nameManager = GameObject.FindGameObjectWithTag ("NameManager").GetComponent<NameManager> ();
		}
		var sortedDict = from entry in nameManager.playerList orderby entry.Value descending select entry;
		int count = nameManager.playerList.Count;
		int index = 0;
		intText.text = "";
		nameText.text = "";


		foreach (KeyValuePair<string, int> value in sortedDict)
		{
			if (index < count && index < 3) {
				nameText.text += ((index + 1).ToString() + "." + value.Key);
				nameText.text += "\n\n\n";
				intText.text += value.Value.ToString();
				intText.text += "\n\n\n";
			}

			index++;
		}
		string intergerText = intText.text;
		string nameTextt = nameText.text;
        if (!isServer)
        {
            CmdUpdateClients(nameTextt, intergerText);
        }
        else
        {
            RpcUpdateClients(nameTextt, intergerText);
        }
    }

	//
	[Command] 
	void CmdUpdateClients(string names, string numbers)
	{
		RpcUpdateClients ( names,  numbers);

	}

	[ClientRpc]
	void RpcUpdateClients(string names, string numbers)
	{	


		nameText.text = names;
		intText.text = numbers;

	
	}
}
