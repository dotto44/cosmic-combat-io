using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBar : MonoBehaviour {
	public Animator shopAnim;
	bool toggle = false;
	public void setHidden(){
		shopAnim.SetBool ("hidden", true);
	}
	public void setVisible(){
		shopAnim.SetBool ("hidden", false);
	}
	public void setVisibleAndHidden(){
		if (toggle) {
			toggle = false;
			shopAnim.SetBool ("hidden", true);
		} else {
			toggle = true;
			shopAnim.SetBool ("hidden", false);
		}

	}
}
