using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timer;
    public int minutes = 9999;
    public void startTimer()
    {
        Debug.Log(minutes);
        minutes++;
        InvokeRepeating("updateTimer", 0f, 60f);
    }
    void updateTimer()
    {
        minutes--;
        int d = minutes / 60 / 24;
        int h = minutes / 60 - d * 24;
        int m = minutes - h * 60;
        string text = "";
        if (d > 0)
        {
            text += d + "d";
        }
        if(h > 0 && d < 2)
        {
            if (text.Length > 0) text += " ";
            text += h + "h";
        }
        if (m > 0 && d == 0 && h < 16)
        {
            if (text.Length > 0) text += " ";
            text += m + "m";
        }
        text += " remaining";
        timer.text = text;
    }
}
