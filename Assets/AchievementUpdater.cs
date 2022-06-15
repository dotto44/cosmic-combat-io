using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUpdater : MonoBehaviour
{
    [SerializeField] StatsHolder holder;
    public void updateAch(int num)
    {
        holder.setAchievementForCollect(num);
        holder.augmentAchievementNum();
    }
}
