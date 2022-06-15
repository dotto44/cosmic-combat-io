using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BirdSwitcher : MonoBehaviour {
	[SerializeField] Animator playerAnim;
	[SerializeField] Animator birdAnimator;
	void Start(){

		if (playerAnim.GetInteger ("costume") == 1) {

			birdAnimator.SetInteger ("costume", 1);
		} 
		if (playerAnim.GetInteger ("costume") == 0) {
          
            birdAnimator.SetInteger ("costume", 0);
		} 

	}
  
    	void OnEnable(){

            if (playerAnim.GetInteger ("costume") == 1) {

                birdAnimator.SetInteger ("costume", 1);
            } 
            if (playerAnim.GetInteger ("costume") == 0) {

                birdAnimator.SetInteger ("costume", 0);
            } 

        }
   
}
