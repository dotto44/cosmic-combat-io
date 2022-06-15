using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementBanner : MonoBehaviour
{
    string[] myDescriptions = {"GET LOST IN SPACE", "REACH A 3 KILL STREAK", "REACH A 5 KILL STREAK",
    "REACH A 10 KILL STREAK", "UNLOCK JUPPERNAUT", "FIRST OUTFIT", "UNLOCK 3 OUTFITS", "FULLY RECOVER FROM NEAR DEATH",
    "GATHER 500 MOON ROCKS", "GATHER 1000 MOON ROCKS", "GATHER 5000 MOON ROCKS", "DON'T STOP DANCING", "REACH A 20 KILL STREAK"};
    string[] myNames = {"OOPSIE!", "DECENTLY DANGEROUS", "PRETTY POWERFUL", "GALATIC GLORY",
    "THE GAS GIANT", "STYLISH", "AMATEUR COLLECTION", "PHEW CLOSE ONE", 
    "SAVINGS ACCOUNT", "PENNY PINCHER", "SCROOGE", "BOOGIE FEVER", "SUPERNOVA"};
    [SerializeField] Sprite[] myIcons;
    [SerializeField] Text description;
    [SerializeField] Text title;
    [SerializeField] Image icon;
    public void setUpBanner(int whatBanner)
    {
        icon.sprite = myIcons[whatBanner];
        title.text = myNames[whatBanner];
        description.text = myDescriptions[whatBanner];
    }
    public void endMe()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

}
