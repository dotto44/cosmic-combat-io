using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSparks.Core;
public class AchievementMonitor : MonoBehaviour
{
    private static bool created = false;
    private string[] keys = { "AWARD_OOPSIE", "AWARD_DECENTLY_DANGEROUS", "AWARD_PRETTY_POWERFUL",
     "AWARD_GALACTIC_GLORY", "AWARD_THE_GAS_GIANT", "AWARD_STYLISH", "AWARD_AMATEUR_COLLECTION",
      "AWARD_PHEW_CLOSE_ONE", "AWARD_SAVINGS_ACCOUNT", "AWARD_PENNY_PINCHER", "AWARD_SCROOGE", "AWARD_BOOGIE_FEVER", "AWARD_SUPERNOVA"};
    [SerializeField] GameObject bannerPrefab;
    private void Awake()
    {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    public void addAchievement(int num)
    {
        if (!GS.Authenticated || StatsHolder.achievementsCompleted[num] == true)
        {
            return;
        }
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey(keys[num]).Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("UNLOCKED ACHIEVEMENT WITH MESSAGE: " + response.JSONString);
                GameObject banner = Instantiate(bannerPrefab);
                banner.GetComponentInChildren<AchievementBanner>().setUpBanner(num);
                Scene scene = SceneManager.GetActiveScene();
                if(scene.name == "Menu")
                {
                    GameObject.FindGameObjectWithTag("AchievementUpdater").GetComponent<AchievementUpdater>().updateAch(num);
                }
                else
                {
                    StatsHolder.achievementsCompleted[num] = true;
                }
            }
            else
            {
                Debug.Log("ERROR: " + response.Errors);
            }
        });
    }
}
