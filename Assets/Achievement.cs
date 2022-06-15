using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Achievement : MonoBehaviour
{ 
    string[] collectTitles = {"AWARD_COLLECT_OOPSIE", "AWARD_COLLECT_DECENTLY_DANGEROUS", "AWARD_COLLECT_PRETTY_POWERFUL",
     "AWARD_COLLECT_GALACTIC_GLORY", "AWARD_COLLECT_THE_GAS_GIANT", "AWARD_COLLECT_STYLISH", "AWARD_COLLECT_AMATEUR_COLLECTION",
      "AWARD_COLLECT_PHEW_CLOSE_ONE", "AWARD_COLLECT_SAVINGS_ACCOUNT", "AWARD_COLLECT_PENNY_PINCHER", "AWARD_COLLECT_SCROOGE", "AWARD_COLLECT_BOOGIE_FEVER", "AWARD_COLLECT_SUPERNOVA"};
    int[] rewardAmts = {10, 50, 100, 250, 50, 30, 50, 50, 50, 100, 500, 50, 500};
    [SerializeField] int achievementNum;
    private bool unlocked = false;
    [SerializeField] GameObject cover;
    [SerializeField] GameObject stardustText;
    [SerializeField] GameObject stardustImage;
    [SerializeField] GameObject checkMark;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Animator collectBox;
    [SerializeField] GameObject collectText;
    [SerializeField] Button collectButton;
    [SerializeField] StatsHolder holder;
    bool collectBoxOn = false;
    [SerializeField] bool isSecret = false;
    [SerializeField] string secretTitle;
    [SerializeField] string secretDescription;
    private void OnEnable()
    {
        if (collectBox.isActiveAndEnabled)
        {
            collectBox.SetBool("collect", collectBoxOn);
        }
    }
    public void uncover()
    {
        collectButton.interactable = false;
        checkMark.SetActive(true);
        stardustImage.SetActive(false);
        stardustText.SetActive(false);
        cover.SetActive(false);
        if (collectBox.isActiveAndEnabled)
        {
            collectBox.SetBool("collect", false);
        }
        collectBoxOn = false;
        collectText.SetActive(false);
        revealSecretText();
    }
    public void coverUp()
    {
        collectButton.interactable = false;
        if (collectBox.isActiveAndEnabled)
        {
            collectBox.SetBool("collect", false);
        }
        collectBoxOn = false;
        checkMark.SetActive(false);
        stardustImage.SetActive(true);
        stardustText.SetActive(true);
        cover.SetActive(true);
        collectText.SetActive(false);

    }
    public void readyToCollect()
    {
        collectButton.interactable = true;
        if (collectBox.isActiveAndEnabled)
        {
            collectBox.SetBool("collect", true);
        }
        collectBoxOn = true;
        checkMark.SetActive(false);
        stardustImage.SetActive(true);
        stardustText.SetActive(true);
        cover.SetActive(false);
        collectText.SetActive(true);
        revealSecretText();
    }
    private void revealSecretText()
    {
        if (!isSecret)
        {
            return;
        }
        title.text = secretTitle;
        description.text = secretDescription;
    }
    public void collectPressed()
    {
        collectButton.interactable = false;
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey(collectTitles[achievementNum]).Send((response) => {
            if (!response.HasErrors)
            {
                uncover();
                bool candidate500 = false;
                bool candidate1000 = false;
                bool candidate5000 = false;
                if (StatsHolder.stardustAmt < 500)
                {
                    candidate500 = true;
                }
                if (StatsHolder.stardustAmt < 1000)
                {
                    candidate1000 = true;
                }
                if (StatsHolder.stardustAmt < 5000)
                {
                    candidate5000 = true;
                }
                StatsHolder.stardustAmt += rewardAmts[achievementNum];
                if (StatsHolder.stardustAmt >= 500 && candidate500)
                {
                    GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(8);
                }
                if (StatsHolder.stardustAmt >= 1000 && candidate1000)
                {
                    GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(9);
                }
                if (StatsHolder.stardustAmt >= 5000 && candidate5000)
                {
                    GameObject.FindWithTag("AchievementMonitor").GetComponent<AchievementMonitor>().addAchievement(10);
                }
                holder.updateStardustText(true);
            }
            else
            {

                collectButton.interactable = true;
            }
        });
    }
}
