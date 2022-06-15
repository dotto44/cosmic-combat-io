using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {
	[SerializeField] Transform cameraTransform;
	[SerializeField] Transform stars2;
	[SerializeField] Transform stars3;
	[SerializeField] Transform stars5;
	Vector3 temp;
	Vector3 last;
	Vector3 first;
	double amt = 1;
	bool firstTime = false;
	// Update is called once per frame
	[SerializeField] float lerpRate = 15;
	void Start(){
		last = cameraTransform.position;
		//gameObject.transform.position = cameraTransform.position;
	}
	void Update () {
		
	//	


		if (last != cameraTransform.position) {
			temp = gameObject.transform.position;
			temp.x += (float)((cameraTransform.position.x - last.x)/amt);
			temp.y += (float)((cameraTransform.position.y - last.y)/amt);
			gameObject.transform.position = temp;
			last = cameraTransform.position;
			if (amt != 1.5) {
				amt = 1.5;
				first = gameObject.transform.position;
			}
			if (gameObject.transform.position.y > first.y) {
				moveStar (stars3, true, false);
				moveStar (stars5, true, false);

			} else if (gameObject.transform.position.y < first.y) {
				moveStar (stars3, false, false);
				moveStar (stars5, false, false);
			}
			if (gameObject.transform.position.x > first.x) {
				moveStar (stars3, true, true);
				moveStar (stars2, true, true);
			} else if (gameObject.transform.position.x < first.x) {
				moveStar (stars3, false, true);
				moveStar (stars2, false, true);
			}
		}
	}
	private void moveStar(Transform starTransform, bool positive, bool xaxis){
		Vector3 temporary = starTransform.localPosition;
		if (xaxis) {
			if (positive) {
				temporary.x = 38.4f;
			} else {
				temporary.x = -38.4f;
			}
		} else {
			if (positive) {
				temporary.y = 38.4f;
			} else {
				temporary.y = -38.4f;
			}
		}
		starTransform.localPosition = temporary;
	}
}
