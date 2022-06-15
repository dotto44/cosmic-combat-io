using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBars : MonoBehaviour
{
    public static readonly int[] characterHealthDisplayValues = { 100, 125, 80, 200, 150};
    [SerializeField] Image pic;

    [SerializeField] Image fuelMeter;
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text plasmaNum;
    [SerializeField] TMP_Text healthNum;

    [SerializeField] Sprite[] normHeadsNormal;
    [SerializeField] Sprite[] roverHeadsNormal;
    [SerializeField] Sprite[] venHeadsNormal;
    [SerializeField] Sprite[] jupHeadsNormal;
    [SerializeField] Sprite[] nepHeadsNormal;

    [SerializeField] Sprite[] normHeadsHurt;
    [SerializeField] Sprite[] roverHeadsHurt;
    [SerializeField] Sprite[] venHeadsHurt;
    [SerializeField] Sprite[] jupHeadsHurt;
    [SerializeField] Sprite[] nepHeadsHurt;

    Sprite[][] headsNormal = new Sprite[5][];
    Sprite[][] headsHurt = new Sprite[5][];

    private const float adjustValueHealth = 90.5f;
    private const float adjustValueFuel = 0.32803f;
    void Start()
    {
       /* headsNormal[0] = normHeadsNormal;
        headsNormal[1] = roverHeadsNormal;
        headsNormal[2] = venHeadsNormal;
        headsNormal[3] = jupHeadsNormal;
        headsNormal[4] = nepHeadsNormal;

        headsHurt[0] = normHeadsHurt;
        headsHurt[1] = roverHeadsHurt;
        headsHurt[2] = venHeadsHurt;
        headsHurt[3] = jupHeadsHurt;
        headsHurt[4] = nepHeadsHurt;
        Debug.Log("SKIN COMBO: " + StatsHolder.characterSelected + " " + StatsHolder.currentSelectedSkin);
        Debug.Log(headsNormal[0].Length);
        pic.sprite = headsNormal[StatsHolder.characterSelected][StatsHolder.currentSelectedSkin];
        */
        healthNum.text = "" + characterHealthDisplayValues[StatsHolder.characterSelected];
    }
    public void setFuelMeter(float amtFuel)
    {
        // fuelMeter.rectTransform.sizeDelta = new Vector2(amtFuel, 60.6f);
        fuelMeter.transform.localPosition = new Vector2(-391.1f + amtFuel * adjustValueFuel - 50, fuelMeter.transform.localPosition.y);
        plasmaNum.text = "" + (int)(amtFuel / 264.00 * 100);
    }
    public void setHealthMeter(double currentHealth, double totalHealth)
    {
        float percentHealth = (float)(currentHealth / totalHealth);
        healthBar.transform.localPosition = new Vector2(-391.1f + percentHealth * adjustValueHealth - 50, healthBar.transform.localPosition.y);
        healthNum.text = "" + Math.Round(currentHealth);
    }
    public void gotHurt()
    {
        //StopAllCoroutines();
        //Debug.Log("hurt index: " + StatsHolder.characterSelected + " " + StatsHolder.currentSelectedSkin);
        //pic.sprite = headsHurt[StatsHolder.characterSelected][StatsHolder.currentSelectedSkin];
        //StartCoroutine(backToNormal());
    }
    IEnumerator backToNormal()
    {
        yield return new WaitForSeconds(0.25f);
        pic.sprite = headsNormal[StatsHolder.characterSelected][StatsHolder.currentSelectedSkin];
    }

}
