using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetBarAmount : MonoBehaviour {
	[SerializeField] Color myColor;
	private Color blurred = new Color (0.3f, 0.3f, 0.3f, 1);
    [SerializeField] TMP_Text label;
    [SerializeField] Image[] bars = new Image[]{};
    private int numBars = 10;
    //IsBlurred is Backwards
    private bool isBlurred = false;
	private Image thisImage;
    Coroutine lastRoutine = null;

   
    // Use this for initialization
     public void setText(string txt)
    {
        label.text = txt;
    }
    public void setBars(int numBars, bool isBlurred){
		if (thisImage == null) {
			thisImage = GetComponent<Image> ();
		}
       /* if (!isActiveAndEnabled)
        {
            gameObject.SetActive(true);
        }*/
        if (!isBlurred) {
			thisImage.color = StatsHolder.CombineColors (myColor, blurred);
		} else {
			thisImage.color = myColor;
		}

       

        // [ ... ]
        if (lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
        }
        this.numBars = numBars;
        this.isBlurred = isBlurred;
        StopCoroutine(fadeInBarsSlowly());
       
        resetColors();
        lastRoutine = StartCoroutine(fadeInBarsSlowly());
	}
    public void resetColors()
    {
        foreach(Image i in bars)
        {
            if (!isBlurred)
            {
                i.color = blurred;
            }
            else
            {
                i.color = Color.white;
            }
          
        }
    }
    IEnumerator fadeInBarsSlowly()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < numBars)
            {
                if (isBlurred)
                {
                    bars[i].color = myColor;
                }
                else
                {
                    bars[i].color = StatsHolder.CombineColors(myColor, blurred);
                }
            }
            else
            {
                if (isBlurred)
                {
                    bars[i].color = Color.white;
                }
                else
                {
                    bars[i].color = blurred;
                }
            }
            yield return new WaitForSeconds(.05f);
        }
       
    }
}
