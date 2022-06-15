using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    static float ANIM_TIME = 0.2f;
    [SerializeField] Animator item;
    [SerializeField] Animator card;
    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text price;
    [SerializeField] TMP_Text description;

    public void showPopup(ShopItem shopItem)
    {
        price.text = "" + shopItem.price;
        name.text = shopItem.name;
        item.CrossFade("BlankItem", 0.0f);
        card.SetBool("visible", true);

        if (shopItem.dance_id == -1)
        {
            card.CrossFade(shopItem.outfit_color, ANIM_TIME);
            description.text = "outfit";
            item.SetInteger("outfit_id", shopItem.outfit_id);
        }
        else
        {
            card.CrossFade(shopItem.dance_color, ANIM_TIME);
            description.text = "dance";
            item.SetInteger("dance_id", shopItem.dance_id);
            item.SetInteger("outfit_id", shopItem.outfit_id);
        }
    }
    public void closePopup()
    {
        card.SetBool("visible", false);
    }
}
