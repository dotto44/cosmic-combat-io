using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveStartdustText : MonoBehaviour
{
    [SerializeField] Text myText;
    [SerializeField] Animator moonRock;
    private int myValue = 0;
    public void animateLabelToCurrent()
    {
        if (myValue < StatsHolder.stardustAmt)
        {
            moonRock.SetBool("jump", true);
        }
        updateLabelAnimated();
    }
    private void updateLabelAnimated()
    {

        if (myValue < StatsHolder.stardustAmt)
        {
            StartCoroutine(waitToUpdate(0.02f, true));
        }
        else if (myValue > StatsHolder.stardustAmt)
        {
            StartCoroutine(waitToUpdate(0.004f, false));
        }
        else
        {
            moonRock.SetBool("jump", false);
        }


    }
    public void updateLabel()
    {
        myText.text = StatsHolder.stardustAmt.ToString("D4");
        myValue = StatsHolder.stardustAmt;
    }
    IEnumerator waitToUpdate(float waitTime, bool up)
    {
        yield return new WaitForSeconds(waitTime);
        if (up)
        {
            if (StatsHolder.stardustAmt - myValue > 400)
            {
                myValue += 20;
            } else if (StatsHolder.stardustAmt - myValue> 100)
            {
                myValue += 10;
            }
            else if (StatsHolder.stardustAmt - myValue> 60)
            {
                myValue += 4;
            }
            else if (StatsHolder.stardustAmt - myValue> 30)
            {
                myValue += 3;
            }
            else if (StatsHolder.stardustAmt - myValue> 10)
            {
                myValue += 2;
            }
            else
            {
                myValue++;
            }

        }
        else
        {
            if (myValue - StatsHolder.stardustAmt > 200)
            {
                myValue -= 20;
            } else if(myValue - StatsHolder.stardustAmt > 100)
            {
                myValue -= 10;
            } else if (myValue - StatsHolder.stardustAmt > 60)
            {
                myValue -= 4;
            }
            else if (myValue - StatsHolder.stardustAmt > 30)
            {
                myValue -= 3;
            } else if (myValue - StatsHolder.stardustAmt > 10)
            {
                myValue -= 2;
            }
            else
            {
                myValue--;
            }
        }
        myText.text = myValue.ToString("D4");
        updateLabelAnimated();
    }
}
