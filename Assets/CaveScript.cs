using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaveScript : MonoBehaviour {
	public Animator cave;
	public Animator mapCave;
    public bool isInOverride = false;
	void OnTriggerEnter2D(Collider2D col){
		if (MethodResource.arrayContains(ServerBulletBase.characterTypes, col.tag)) {

            if (col.name == GetName.userName) {
                if (!isInOverride)
                {
                    cave.SetBool("faded", true);
                    mapCave.SetBool("faded", true);
                }
			}
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (MethodResource.arrayContains(ServerBulletBase.characterTypes, col.tag)) {

            if (col.name == GetName.userName) {
                if (!isInOverride)
                {
                    cave.SetBool("faded", false);
                    mapCave.SetBool("faded", false);
                }
			}
		}
	}
	public void resetInCase(){
      
        cave.CrossFade("Normal", 0.0f);
        mapCave.CrossFade("Normal", 0.0f);
        cave.SetBool("faded", false);
        mapCave.SetBool("faded", false);
    }
    public void turnOnInCase()
    {
        cave.SetBool("faded", true);
        mapCave.SetBool("faded", true);
    }
}
