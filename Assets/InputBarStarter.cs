using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputBarStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GetName.userName.Length != 0 && GetName.userName != "Player") {
			gameObject.GetComponent<InputField> ().text = GetName.userName;
		}
	
	}

}
