using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Killfeed : MonoBehaviour {
  
    public static readonly string[] killMethodNums = 
     {
          "SPACE",
          "EARTHLING - NORMAL",
          "ROVER - NORMAL",
          "VENUSIAN - NORMAL",
          "VENUSIAN - OWL",
          "JUPPERNAUT - NORMAL",
          "JUPPERNAUT - EXPLOSION",
          "JUPPERNAUT - FIRE",
          "NEPTUNIAN - NORMAL",
          "NEPTUNIAN - SUPER",
          "ICICLE"
     };
    public static readonly string[][] messageTags =
   {
          new string[]{ "<i><size=14><color=#A76BEF>", "</color></size></i>"},
          new string[]{ "<i><size=14><color=#FFFF59>", "</color></size></i>"},
          new string[]{ "<i><size=14><color=#4FFF50ff>", "</color></size></i>"},
          new string[]{ "<i><size=14><color=#FF98D0>", "</color></size></i>"},
          new string[]{ "<i><size=14><color=#FF98D0>", "</color></size></i>"},
          new string[]{ "<i><size=14><color=#FF5400ff>", "</color></size></i>" },
          new string[]{ "<i><size=14><color=#FF5400ff>", "</color></size></i>" },
          new string[]{ "<i><size=14><color=#FF5400ff>", "</color></size></i>" },
          new string[]{ "<i><size=14><color=#4BC8D3>", "</color></size></i>" },
          new string[]{ "<i><size=14><color=#4BC8D3>", "</color></size></i>" },
          new string[]{ "<i><size=14><color=#A3CAF0>", "</color></size></i>"}

     };
    public static readonly string[][] messageTagsDescription =
 {
          new string[]{ "<i><size=20><color=#A76BEF>", "</color></size></i>"},
          new string[]{ "<i><size=20><color=#FFFF59>", "</color></size></i>"},
          new string[]{ "<i><size=20><color=#4FFF50ff>", "</color></size></i>"},
          new string[]{ "<i><size=20><color=#FF98D0>", "</color></size></i>"},
          new string[]{ "<i><size=20><color=#FF98D0>", "</color></size></i>"},
          new string[]{ "<i><size=20><color=#FF5400ff>", "</color></size></i>" },
          new string[]{ "<i><size=20><color=#FF5400ff>", "</color></size></i>" },
          new string[]{ "<i><size=20><color=#FF5400ff>", "</color></size></i>" },
          new string[]{ "<i><size=20><color=#4BC8D3>", "</color></size></i>" },
          new string[]{ "<i><size=20><color=#4BC8D3>", "</color></size></i>" },
          new string[]{ "<i><size=20><color=#A3CAF0>", "</color></size></i>"}

     };
    public static readonly string[][] killMethods =
     {
          new string[]{"LOST IN SPACE"},
          new string[]{"ZAPPED", "DISINTEGRATED", "SHATTERED", "ELIMINATED", "YEETED"},
          new string[]{"AMBUSHED", "DEVASTATED", "DESTROYED", "BUSHWHACKED"},
          new string[]{"SHOT", "SNIPED", "NO-SCOPED"},
          new string[]{"CLAWED"},
          new string[]{"OBLITERATED", "ANNIHILATED", "INCINERATED", "EXPLODED" },
          new string[]{"OBLITERATED", "ANNIHILATED", "INCINERATED", "EXPLODED" },
          new string[]{"OBLITERATED", "ANNIHILATED", "INCINERATED", "EXPLODED" },
          new string[]{"SPLASHED", "SCALDED", "FLOODED", "FISH-KEBAB'D"},
          new string[]{"BUBBLE BOPPED"},
          new string[]{"IMPALED", "SKEWERED", "ICICLE'D", "GIVEN THE COLD SHOULDER"}

     };

    public static readonly int[][] killValues =
    {
          new int[]{1}, //SPACE
          new int[]{100, 40, 80, 60, 3}, //NORM
          new int[]{100, 50, 50, 20 }, //ROVER
          new int[]{100, 30, 3}, //VEN
          new int[]{100},//VEN OWL
          new int[]{80, 80, 50, 20}, //JUP
          new int[]{80, 80, 50, 20},
          new int[]{80, 80, 50, 20},
          new int[]{80, 70, 50, 10},
          new int[]{100},
          new int[]{100, 50, 50, 20},
     };
  //  [SerializeField] TMP_Text[] messages;
    [SerializeField] GameObject[] messageObjs;
   //[SerializeField] Animator[] messageAnims;
    private bool[] usedMessages = { false, false, false, false };
    //  private static readonly string[]
    public string feedText = "";
	private string oldText;
	private string newText;
	private Text feedTextComponent;
	private int numLines = 0;
    [SerializeField] Animator anim;
    [SerializeField] TMP_Text barText;
    //	void Start () {
    //	feedTextComponent = GetComponent<Text> ();
    //	feedTextComponent.text = feedText;
    //}
    private void Start()
    {
       // addKillToFeed("GAY BOI", "shot", "MY MOM", "EARTHLING - NORMAL");
      //  addKillToFeed("bruh", "shot", "ROVER", "ROVER - NORMAL");
       // addKillToFeed("COOL JOHN", "shot", "JUPPERNAUT", "JUPPERNAUT - NORMAL");
    }
    public void addKillToFeed(string who, string how, string byWho, string whatMethod){
        /*	numLines++;

            if (byWho != null && byWho != "") {
                feedTextComponent.text += "\n" + byWho + " zapped " + who;
            } else {
                feedTextComponent.text += "\n" + who + " got lost in space";
            }
            if (numLines >= 4) {
                oldText = feedTextComponent.text;
                int index = oldText.IndexOf (System.Environment.NewLine);
                newText = oldText.Substring (index + System.Environment.NewLine.Length);
                feedTextComponent.text = newText;
                numLines--;
            }
            feedText = feedTextComponent.text; */
        string killWord = getKillWord(whatMethod, false, false);
        string killMessage = killWord;
        if (whatMethod == "SPACE")
        {
            killMessage = who + " <i><size=14><color=#A76BEF>WAS</color></size></i> " + killMessage;
        }
        else if(whatMethod == "ICICLE")
        {
            killMessage = who + " <i><size=14><color=#A3CAF0>WAS</color></size></i> " + killMessage;
        }
        else
        {
            killMessage = byWho + " " + killMessage + " " + who;
        }

        // messages[2].text = messages[1].text;
        //  messages[1].text = messages[0].text;
        // messages[0].text = killMessage;
        messageObjs[2].GetComponent<TMP_Text>().text = killMessage;
        GameObject temp = messageObjs[2];
        messageObjs[2] = messageObjs[1];
        messageObjs[1] = messageObjs[0];
        messageObjs[0] = temp;

      

        messageObjs[0].transform.localPosition = new Vector3(0, -8f, 0);
        messageObjs[1].transform.localPosition = new Vector3(0, -1f, 0);
        messageObjs[2].transform.localPosition = new Vector3(0, 6f, 0);

        messageObjs[0].GetComponent<Animator>().CrossFade("FadeOutAfter3", 0f);
        //feedTextComponent.fontSize = 24 - who.Length;
        if(byWho == GetName.userName)
        {
            barText.text = " " + killWord + " " + who;
            anim.CrossFade("FadeOutAfter3", 0f);
        }
       
    }
/*	public void UpdateGameText(string _string){
		Debug.Log ("Updating the text so hook ran");

		//if (!isServer) { 
			feedText = _string;
		//}
		feedTextComponent.text = feedText;
	}*/
    public static string getKillWord(string whatMethod, bool isDeathUI, bool plain)
    {
        int whosKeyWords = 0;
        for(int i = 0; i < killMethodNums.Length; i++)
        {
            if (whatMethod.Equals(killMethodNums[i]))
            {
                whosKeyWords = i;
                break;
            }
        }
        int total = 0;
        foreach(int j in killValues[whosKeyWords])
        {
            total += j;
        }

        int randValue = Random.Range(1, total);
        int index = 0;
        Debug.Log("TOTAL GOING IN: " + total);
        while(randValue > 0)
        {
            randValue -= killValues[whosKeyWords][index];
            Debug.Log("MINUS: " + killValues[whosKeyWords][index]);
            if (randValue > 0)
            {
                Debug.Log("INCREASE INDEX");
                index++;
            }
        }
        Debug.Log("WHOS KEY WORD: " + whosKeyWords);
        Debug.Log("INDEX: " + index);
        if (isDeathUI)
        {
            return messageTagsDescription[whosKeyWords][0] + killMethods[whosKeyWords][index] + messageTagsDescription[whosKeyWords][1];
        }
        if (plain)
        {
            return killMethods[whosKeyWords][index];
        }
        return messageTags[whosKeyWords][0] + killMethods[whosKeyWords][index] + messageTags[whosKeyWords][1];
    }
}

/*
	[SyncVar] private string killFeedTex = "";
	private string lastValue = "";
	void FixedUpdate(){

		TransmitKillfeed ();
		LerpRotation ();
	}
	void LerpRotation(){
		if (!isLocalPlayer) {
			playerRenderer.flipX = syncFlipped;
			if (syncFlipped != lastSyncFlipped) {

				lastSyncFlipped = syncFlipped;
				if (syncFlipped) {

					Vector3 pos = armTransform.localPosition;
					pos.x = -0.31f; 
					armTransform.localPosition = pos;
					armRenderer.flipY = true;
				} else {

					Vector3 pos = armTransform.localPosition;
					pos.x = 0.31f; 
					armTransform.localPosition = pos;
					armRenderer.flipY = false;
				}
			}
			if (playerAnim.GetBool ("shooting") != lastArmActive) {
				lastArmActive = playerAnim.GetBool ("shooting");
				if (lastArmActive) {
					arm.SetActive (true);
				} else {
					arm.SetActive (false);
				}
			}

			Vector3 armZRotation = new Vector3 (0, 0, syncZRot); 
			armTransform.localRotation = Quaternion.Lerp (armTransform.rotation, Quaternion.Euler(armZRotation), Time.deltaTime * 30);

		}
	}

	[Command]
	void CmdProvideRotationsToServer(bool flip){
		syncFlipped = flip;
	}
	[Command]
	void CmdProvideRotationsToServerArm(float zPos){

		syncZRot = zPos;
	}
	[ClientCallback]
	void TransmitKillfeed(){
		if (isLocalPlayer &&  != lastValue) {
			CmdProvideRotationsToServer (playerRenderer.flipX);
			lastValue = playerRenderer.flipX;
		}
		if (isLocalPlayer && arm.activeSelf) {

			CmdProvideRotationsToServerArm (armTransform.localEulerAngles.z);
			lastRotation = armTransform.localEulerAngles.z;
		}
	}

}*/

