using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceholderName : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GetName.userName != null && GetName.userName != ""  && GetName.userName != "Player") {
			GetComponent<Text> ().text = GetName.userName;
		}
	}

}
