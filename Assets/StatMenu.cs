using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMenu : MonoBehaviour
{
    [SerializeField] StatsHolder holder;
    private void OnEnable()
    {
        holder.WhenEnabld();

    }
}
