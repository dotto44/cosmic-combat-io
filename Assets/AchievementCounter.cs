using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementCounter : MonoBehaviour
{
    [SerializeField] Text myText;
    const int total = 13;
    int numFinished = 0;
    public void setCount(int num)
    {
        numFinished = num;
        myText.text = "" + numFinished + "/" + total;
    }
    public void augmentAchievementNum()
    {
        numFinished++;
        myText.text = "" + numFinished + "/" + total;
    }
}
