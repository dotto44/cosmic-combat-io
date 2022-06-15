using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Boulder : NetworkBehaviour {
    private const float rock_health = 5; 
	private float rockHealth = rock_health;
	private int rockStage = 6;

	public float stageResetValue = 0;
	[SerializeField] Animator boulderAnim;
    [SerializeField] AudioSource audioData;
    [SerializeField] GameObject test;
    [SerializeField] Color[] boulderColors;

    public void hitRock(int amount)
	{
		if (!isServer) {
			return;
		}
		rockHealth -= amount;
		if (rockHealth <= 0) {
            //  audioData.Play(0);
            var enemy = Instantiate(test, new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), Quaternion.Euler(rockStage, 0, 48));
            //enemy.transform.parent = null;
            NetworkServer.Spawn(enemy);
          
          
            rockStage--;
			rockHealth = rock_health;
			if (rockStage <= stageResetValue) {
				boulderAnim.SetInteger ("stage",7);
				rockStage = 7;
				StartCoroutine (yah ());
			} else {
				boulderAnim.SetInteger ("stage",rockStage);
			}
		}
	}
	private IEnumerator yah(){
		yield return new WaitForSeconds(25);
		StartCoroutine (yeet ());
	}
	private IEnumerator yeet(){
		bool shouldRespawn = false;
		while (!shouldRespawn) {
			Debug.Log ("STARTING CHECK FOR RESPAWN");
			yield return new WaitForSeconds(5.0f);
			// shouldRespawn = ScanForItems ();
		}
		boulderAnim.SetInteger ("stage",6);
		rockStage = 6;
		Debug.Log ("RESPAWN");
	}

	/*	private bool ScanForItems()
		{
			Debug.Log ("SCANNING");
			bool returnType = true;
			Collider2D[] allOverlappingColliders = Physics2D.OverlapCircleAll (gameObject.transform.position, 7);
			foreach(Collider2D hit in allOverlappingColliders){
				if (MethodResource.arrayContains(ServerBulletBase.characterTypes, hit.tag)) {

					returnType = false;
					Debug.Log ("FOUND PLAYER");
				}
			}
			Debug.Log ("DIDNT FIND PLAYER");
			return returnType;
		}*/
}
