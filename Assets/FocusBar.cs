using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusBar : MonoBehaviour {
	[SerializeField] Health playerHealth;
	public void usedFocus(){
		if (StatsHolder.characterSelected == 0) {
			playerHealth.AddHealth (25);
		}
		if (StatsHolder.characterSelected == 1) {
			playerHealth.AddHealth (25);
		}
		if (StatsHolder.characterSelected == 2) {
			playerHealth.AddHealth (25);
		}
	}
}
