using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipText : MonoBehaviour {
	[SerializeField] Text tip;
	string[] saying = new string[]{
		"DID YOU KNOW EACH CHARACTER HAS A UNIQUE SPECIAL ABILITY? TRY HOLDING DOWN SPACE.",
		"PRESS F TO CONVERT PLASMA INTO HEALTH.",
		"ROVER IS THE BEST AT CLOSE COMBAT!",
		"PLASMA IS ESSENTIAL! PICK UP BLUE PLASMA STARS TO REPLENISH YOUR SUPPLY.",
		"PRESS SHIFT TO TAUNT YOUR OPPONENTS. ASTRONOMICAL HUMILIATION!",
		"LIKE OUR GAME? PLEASE FOLLOW US ON TWITTER @JUDOGAMES!",
		"HATE OUR GAME? GIVE US IDEAS ON TWITTER @JUDOGAMES!",
		"EARTHLING IS A JACK OF ALL TRADES, MASTER OF NONE.",
		"PRESS DOWN/S TO DROP THROUGH BRIDGES AND PLATFORMS.",
        "YOU CAN VIEW A LARGE MAP TO TRACK YOUR OPPONENTS. PRESS M OR RIGHT CLICK.",
		"THE CYBUNNIES HAVE BEEN STRANDED ON THE MOON SINCE THEIR SHIP CRASHED."
	};
	public void newTip(){
		int randomNum = Random.Range (0, saying.Length);
		tip.text = saying [randomNum];
	}
}
