using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class NameManager : NetworkBehaviour {
	//public List<string> playerList = new List<string>();
	public Dictionary<string, int> playerList = new Dictionary<string, int>();
	private Dictionary<int, string> idWithNames = new Dictionary<int, string>();
	 private LeaderBoard leaderBoard; 
	public Player_ID playerId;

	public void addPlayer(string _player, int id, Player_ID script, bool CanChange)
	{
		if (GameObject.Find ("Leaderboard(Clone)") != null) {
			leaderBoard = GameObject.Find ("Leaderboard(Clone)").GetComponent<LeaderBoard> ();
		}
		bool shouldAddKiller = true;
		/*foreach(var player in playerList) 
		{
			if (player.Key == _player) 
			{
				shouldAddKiller = false;
			}
		}*/
		if (shouldAddKiller) {
		
			int inCase = 1;
			string newPlayer = _player;
           if (CanChange) {
            if (newPlayer != "")
            {
                do
                {
                   
                    newPlayer = _player;
                    //if(playerList.ContainsKey (newPlayer)){
                    if (inCase >= 2)
                    {
                        newPlayer += inCase.ToString();

                    }
                    //}

                    inCase++;
                } while (playerList.ContainsKey(newPlayer));
                if (_player != newPlayer)
                {
                    //TargetDoMagic(target, newPlayer);
                    script.userNameLocal = newPlayer;
                    script.adjustName(newPlayer);
                }
                playerList.Add(newPlayer, 0);
                idWithNames.Add(id, newPlayer);
            }
               
           }
       
			//	idWithNames.Add(
			//	leaderBoard = GameObject.Find ("Leaderboard(Clone)").GetComponent<LeaderBoard> ();
				if (leaderBoard != null) {
					leaderBoard.updateLeaderboardFirst ();
				}
			
		}
	}

	public void removePlayer(int id)
	{
		//playerList dictionary associates the username with their kill count
		//idWithNames dictionary associates the connectionId with the username 
		if (playerList.Keys.Contains(idWithNames[id])) //Check to make sure the player is on the leaderboard, should always be true
        {
			playerList.Remove(idWithNames[id]); //Remove player from leaderboard
			leaderBoard.GetComponent<LeaderBoard>().updateLeaderboardFirst(); //Update the leaderboard text, passes RPC to clients
			idWithNames.Remove(id); //Removes player connection from the ID list
		}
	}
	/*[TargetRpc]
	public void TargetDoMagic(NetworkConnection target, string extra)
	{
		GetName.userName = extra;
		//playerId.adjustName (extra);
	}*/
}
