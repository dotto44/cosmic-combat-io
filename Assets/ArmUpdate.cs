using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ArmUpdate : NetworkBehaviour {

	[SyncVar] float syncZRot = 0;

	[SerializeField] private Transform armTransform;
	[SerializeField] float lerpRate = 15;

	private float lastRotation;

	public GameObject player;
	public GameObject arm;
	private bool lastArmActive = false;
	//	[SerializeField] private float lerpRate = 15;
	// Use this for initialization
	void FixedUpdate(){
		if (gameObject.activeSelf) {
			Debug.Log ("IsChecking");
			TransmitRotations ();
			LerpRotation ();
		}
	}
	void LerpRotation(){
		if (!isLocalPlayer) {
			Quaternion armZRotation = new Quaternion (armTransform.rotation.x, armTransform.rotation.y, syncZRot, armTransform.rotation.w); 
			armTransform.rotation = Quaternion.Lerp (armTransform.rotation, armZRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationsToServer(float zPos){
		Debug.Log ("SendingToServer");
		syncZRot = zPos;
	}
	[ClientCallback]
	void TransmitRotations(){
		Debug.Log ("What rotation?");
		Debug.Log(Mathf.Abs(Mathf.DeltaAngle(armTransform.rotation.z, lastRotation)));
		Debug.Log (isLocalPlayer);
		if (Mathf.Abs(Mathf.DeltaAngle(armTransform.rotation.z, lastRotation)) > 0.05) {
			Debug.Log ("TransmittingClientRotations");
			CmdProvideRotationsToServer (armTransform.rotation.z);
			lastRotation = armTransform.rotation.z;
		}
	}
}
