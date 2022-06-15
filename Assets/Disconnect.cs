using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Disconnect : MonoBehaviour {
	private HostGame manager;

	public void Start(){
		manager = GameObject.Find("CustomNetworkManager").GetComponent<HostGame>();


	}
	public void leaveRoom(){
		/*MatchInfo matchInfo = networkManager.matchInfo;
		networkManager.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
		networkManager.StopHost();*/
		manager.stopGame();
	}
}
