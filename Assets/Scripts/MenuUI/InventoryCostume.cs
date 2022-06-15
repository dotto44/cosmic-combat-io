using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryCostume : MonoBehaviour
{
    [SerializeField] Image outfit;
    [SerializeField] RectTransform outfitT;
    [SerializeField] Image back;
    [SerializeField] Button btn;
    [SerializeField] CharacterSelect characterSelect;
    int id = 0;

    public void set(Sprite backS, Sprite outS, int id, bool active)
    {
        
        if(outS == null)
        {
            outfit.enabled = false;
        }
        else
        {
            outfit.enabled = true; 
            outfit.sprite = outS;
            outfitT.sizeDelta = new Vector2(SkinInventory.OUTFIT_WINDTH_VALUES[CharacterSelect.characterSelected], outfitT.sizeDelta.y);
        }
        back.sprite = backS;
        btn.interactable = active;
        this.id = id;
    }
    public void pressed()
    {
        characterSelect.setCostume(id, transform);
    }
}
