using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HostGame : MonoBehaviour {

	[SerializeField] LoginWindow logger;

	public static string serverIPaddress = "";
	NetworkManager manager;

	void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}

	public void startGame(){
		if (StatsHolder.unlockedChars [StatsHolder.characterSelected]) {
			if (!NetworkClient.active)
			{
				manager.StartClient();
            }
            else
            {
				Debug.Log("ERROR WITH START CLIENT: CLIENT ACTIVE");
            }
		} else {
			logger.turnOnWindow ();
		}
	}
    public void stopGame()
    {
        if (NetworkClient.active)
        {
			manager.StopClient();
		}
	}
    /*public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
	/*	int numberOfMatches = 0;
		if (matchSearch) {
			if (success) {
				Debug.Log ("Listed Matches");
				if (matches != null) {
					Debug.Log ("There is at least 1 match");
					Debug.Log (matches);
					foreach (var match in matches) {
						Debug.Log ("Match Full");
						if (match.currentSize < match.maxSize) {
							numberOfMatches++;
							Debug.Log ("Found Match to Join");
							networkManager.matchName = match.name;
							networkManager.matchSize = (uint)match.currentSize;
							networkManager.matchMaker.JoinMatch (match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
							matchSearch = false;
							return;
						}
					}
					if (numberOfMatches == 0) {
						Debug.Log ("Calling create room function (after checking list)");
						CreateRoom (matches.Count);
					}
				} else {
					Debug.Log ("Calling create room function");
					CreateRoom (matches.Count);
				}
			} else {
				Debug.Log ("Failed to get matches");
			}
		}
	}
	public void SetRoomName(string _name)
	{
		roomName = _name;
	}
	public void Join(){
		Debug.Log ("Run Match Search");
		matchSearch = true;
	if (networkManager.matchInfo == null) 
		{
			if (networkManager.matches == null) 
			{
				Debug.Log ("Call List Matches");
				networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);

			} 
		}
	}
	public void OnMatchList(){

	}
	public void CreateRoom(int numberOfRooms){
		SetRoomName ((numberOfRooms + 1).ToString());
		if (roomName != "" && roomName != null) {
			Debug.Log ("Creating Room:" + roomName);
			networkManager.matchMaker.CreateMatch (roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
		}
	}
*/

   
}
