using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class ShopItem : MonoBehaviour
{
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text description;
    [SerializeField] Animator card;
    [SerializeField] Animator item;

    public int price;
    public string name;
     
    public int outfit_id = -1;
    public int character_id = -1;
    public int dance_id = -1;

    public string outfit_color;
    public string dance_color;

    public string getName()
    {
        return name;
    }
    public void updateInfo()
    {
        priceText.text = "" + price;
        nameText.text = name;
        if (dance_id == -1)
        {
            description.text = "outfit";
        }
        else
        {
            description.text = "dance";
        }
    }
    private void OnEnable()
    {
        if(dance_id == -1)
        {
            card.CrossFade(outfit_color, 0.0f);
            item.SetInteger("outfit_id", outfit_id);
        }
        else
        {
            card.CrossFade(dance_color, 0.0f);
            item.SetInteger("dance_id", dance_id);
        }
    }
}
