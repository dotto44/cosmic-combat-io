using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour {
	[SerializeField] DeathUI deathUi;
	void OnEnable(){
		deathUi.pastCharacter = StatsHolder.characterSelected;
		//Debug.Log ("UPDATED FROM HOLDER");
	}
}
