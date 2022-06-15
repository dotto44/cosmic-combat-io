using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusianArmScript : MonoBehaviour {
	[SerializeField] Sprite defaultArm;
	[SerializeField] Sprite guardArm;
	[SerializeField] Animator playerAnim;
	[SerializeField] Animator birdAnimator;
	[SerializeField] SpriteRenderer armImage;
	[SerializeField] PlayerController player;
    [SerializeField] Animator armAnim;
	int previousInt = 0;
	void OnEnable(){
		
		if (playerAnim.GetInteger ("costume") == 1 && previousInt != 1) {
			changeToSanta ();
			previousInt = 1;
		} 
		if (playerAnim.GetInteger ("costume") == 0 && previousInt != 0) {
			changeToDefault ();
			previousInt = 0;
		} 

	}

	public void changeToSanta(){
		armImage.sprite = guardArm;
	}
	public void changeToDefault(){
		armImage.sprite = defaultArm;
	}
    public void disableShoot()
    {

        armAnim.SetBool("shoot", false);
    }
}
