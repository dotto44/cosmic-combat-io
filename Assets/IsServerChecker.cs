using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class IsServerChecker : NetworkBehaviour {
	[SerializeField] GameObject leader;
    [SerializeField] GameObject A_sharp_pathfinding;
	// Use this for initialization
	void Start () {
		if (isServer) {
            Instantiate(A_sharp_pathfinding);
			var player = Instantiate(leader) as GameObject;     
			NetworkServer.Spawn (player);
		}
	}

}
