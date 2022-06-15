using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinBox : MonoBehaviour
{
    [SerializeField] Animator charAnim;
    [SerializeField] Animator myAnim;
    [SerializeField] RectTransform rectTransform;
     private RuntimeAnimatorController ran;

    [SerializeField] Text rarityLabel;
    private string rarity;
    private int size = 192;
    public void SetLiner(RuntimeAnimatorController ran)
    {
        this.ran = ran;
    }
    public void SetSize(int size)
    {
        this.size = size;
    }
    public void swapCharacter()
    {
        myAnim.SetBool("swap", false);
        swapText();
        charAnim.runtimeAnimatorController = ran;
        rectTransform.sizeDelta = new Vector2(size, size);
    }
    private void swapText() {
        if (rarity.Equals(""))
        {
            return;
        }
        rarityLabel.text = rarity.ToUpper();
        rarityLabel.color = getColor(rarity);
    }

    public void SetRarity(string rarity) {
        this.rarity = rarity;
    }
    public void SetRarity(int rarity)
    {
        if (rarity == 0)
        {
            this.rarity = "Default";
        }
        else if (rarity == 1)
        {
            this.rarity = "Rare";
        }
        else if (rarity == 2)
        {
            this.rarity = "Epic";
        }
        else if (rarity == 3)
        {
            this.rarity = "Legendary";
        }
        else if (rarity == 4)
        {
            this.rarity = "N.A.";
        }
    }
    private Color getColor(string rarityString)
    {
        if(rarityString == "Default")
        {
            return new Color(87/255f,161 / 255f, 250 / 255f);
        }
        if (rarityString == "Rare")
        {
            return new Color(148 / 255f, 248 / 255f, 114 / 255f);
        }
        if (rarityString == "Epic")
        {
            return new Color(196 / 255f, 91 / 255f, 240 / 255f);
        }
        if (rarityString == "Legendary")
        {
            return new Color(250 / 255f, 240 / 255f, 86 / 255f);
        }
        return new Color(0 / 255f, 0 / 255f, 0 / 255f);
    }

}
