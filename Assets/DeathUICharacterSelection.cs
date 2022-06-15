using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUICharacterSelection : MonoBehaviour {
	
	[SerializeField] Text stardustLabel;

    [SerializeField] DeathUI deathUI;   
	// Use this for initialization
	public void updateStardustText(){
		stardustLabel.text = StatsHolder.stardustAmt.ToString("D4");
	}

	/*public void clickedSkin(int index)
    {
        StatsHolder.changeCharacterSelected(index);
        bool anyCostumesUnlocked = false;
        foreach(bool b in StatsHolder.allCostumes[index])
        {
            if (b)
            {
                anyCostumesUnlocked = true;
                return;
            }
        }
        if (!anyCostumesUnlocked)
        {
            StatsHolder.currentSelectedSkin = 0;
            deathUI.pressedCharMenu();
        }
    }
    public void defaultNorm(){
        StatsHolder.changeCharacterSelected(0);
        StatsHolder.currentSelectedSkin = 0;
    }
	public void santaNorm(){
        StatsHolder.changeCharacterSelected(0);
        StatsHolder.currentSelectedSkin = 1;
    }
	public void goldNorm(){
        StatsHolder.changeCharacterSelected(0);
        StatsHolder.currentSelectedSkin = 3;
    }
	public void defaultRover(){
        StatsHolder.changeCharacterSelected(1);
        StatsHolder.currentSelectedSkin = 0;
    }
	public void tuxedoRover(){
        StatsHolder.changeCharacterSelected(1);
        StatsHolder.currentSelectedSkin = 1;
    }
	public void defaultVen(){
        StatsHolder.changeCharacterSelected(2);
        StatsHolder.currentSelectedSkin = 0;
    }
	public void guardVen(){
        StatsHolder.changeCharacterSelected(2);
        StatsHolder.currentSelectedSkin = 1;
    }
    public void defaultJup()
    {
        StatsHolder.changeCharacterSelected(3);
        StatsHolder.currentSelectedSkin = 0;
    }*/
}
