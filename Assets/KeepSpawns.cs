using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSpawns : MonoBehaviour {
    private static bool created = false;
	// Use this for initialization
	void Awake () {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }
	

}
