using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weird : MonoBehaviour {
	[SerializeField] GameObject cameraMini;
	// Use this for initialization


	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp.y = cameraMini.transform.position.y - cameraMini.GetComponent<RectTransform> ().rect.height / 2 - gameObject.GetComponent<RectTransform> ().rect.height / 2;
		transform.position = temp;
	}
}
