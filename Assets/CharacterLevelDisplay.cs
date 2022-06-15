using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterLevelDisplay : MonoBehaviour
{
    [SerializeField] bool isMenu;
    [SerializeField] bool isDeathUI;

    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text toGoText;
    [SerializeField] RectTransform levelBar;


    [SerializeField] Animator banner;
    [SerializeField] StatsHolder statsHolder;
    //LEVEL SYSTEM IS CURRENTLY INACTIVE
    /* void Start()
    {
        if (isMenu)
        {
            RecordHolder.calcStartingLevels();
            updateLevelText();
        }
        if (isDeathUI)
        {
            statsHolder.setBottomBarImages();
        }
        //   StartCoroutine(DoCheck());
    }*/


    public void updateLevelText()
    {
      //  if (!isDeathUI)
      //  {
            levelText.text = "LEVEL " + RecordHolder.getLevel(StatsHolder.characterSelected);
            toGoText.text = RecordHolder.getRemaining(StatsHolder.characterSelected) + "<size=20> </size><size=30>TO</size><size=20> </size><size=30>GO</size>";
            levelBar.sizeDelta = new Vector2(RecordHolder.getLengthOfBar(StatsHolder.characterSelected), 53.56f);
            banner.SetInteger("num", RecordHolder.getBanner(StatsHolder.characterSelected));
            banner.CrossFade("MainState", 0f);
       //}
        // toGoText.text = 
    }
    public void updateLevelTextWith(int character)
    {
       // if (!isDeathUI)
       // {
            levelText.text = "LEVEL " + RecordHolder.getLevel(character);
            toGoText.text = RecordHolder.getRemaining(character) + "<size=20> </size><size=30>XP TO</size><size=20> </size><size=30>GO</size>";
            levelBar.sizeDelta = new Vector2(RecordHolder.getLengthOfBar(character), 53.56f);
            banner.SetInteger("num", RecordHolder.getBanner(character));
            banner.CrossFade("MainState", 0f);
      // }
    }
    
    // Update is called once per frame
    IEnumerator DoCheck()
    {
        for (; ; )
        {
            Debug.Log("UPDATING WITH XP: " + RecordHolder.getXPForChar(0));
            RecordHolder.addXPtoCharacter(0, 1);
            updateLevelText();
            yield return new WaitForSeconds(.5f);
        }
    }

}
