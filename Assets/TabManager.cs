using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] Tab[] tabs;

    public void resetTabs()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].selected)
            {
                tabs[i].unselect();
            }
        }
    }
}
