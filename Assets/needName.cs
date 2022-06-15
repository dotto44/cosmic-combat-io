using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needName : MonoBehaviour {

	// Use this for initialization
	public void turnOffCallBack(){
		GetComponent<Animator> ().SetBool ("need", false);
	}

}
