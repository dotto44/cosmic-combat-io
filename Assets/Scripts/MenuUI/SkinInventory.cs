using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinInventory : MonoBehaviour
{
    [SerializeField] InventoryCostume[] buttons;
    [SerializeField] Transform defaultTransform;
    public InventoryItemData[,] inventoryItems = new InventoryItemData[CharacterSelect.NUMBER_OF_CHARACTERS, 17];
    [SerializeField] Sprite[] backs;
    [SerializeField] Sprite[] outfits;

    public static int[] OUTFIT_WINDTH_VALUES = { 128, 104, 128, 192, 136 };

    public static string[] CHARACTER_DESCRIPTIONS = { "\"THE EARTHLING\"", "\"THE ROVER\"", "\"THE VENUSIAN\"", "\"THE JUPPERNAUT\"", "\"THE NEPTUNIAN\"" };
    public static string[] OUTFIT_DESCRIPTIONS = { "", "\"THE GIFT GIVER\"", "\"THE GOLDEN BOY\"", "\"THE DEFENDER\"", "\"THE SWASHBUCKLER\"", "\"THE DETECTIVE\"", "\"THE SCRAP-HEAP\"" , "\"LOOKING SPIFFY\"" };

    int previousLength = 17;
    int length = 17;

    public Transform findSelectedCostume()
    {
        for (int i = 0; i < 17; i++)
        {
            if (inventoryItems[CharacterSelect.characterSelected, i] == null)
            {
                break;
            }
            else if (inventoryItems[CharacterSelect.characterSelected, i].outfit_id == CharacterSelect.costumeSelected[CharacterSelect.characterSelected])
            {
                return buttons[i].transform;
            }
        }
        return defaultTransform;
    }
    public void updateInventory()
    {
        for(int i = 0; i < 17; i++)
        {
            Debug.Log("qqq " + i);
            InventoryItemData currentBtn = inventoryItems[CharacterSelect.characterSelected,i];
            if(currentBtn == null)
            {
                length = Math.Min(length, i);
                buttons[i].set(backs[0], null, 0, false);
                if (previousLength <= i)
                {
                    break;
                }
            }
            else
            {
                buttons[i].set(backs[(int)currentBtn.rarity], outfits[currentBtn.outfit_id], currentBtn.outfit_id, true);
            }
        }
        previousLength = length;
        length = 17;
    }
    public void addSkinToInventory(int character_id, InventoryItemData skinData)
    {
        for (int i = 0; i < 17; i++)
        {
            if (inventoryItems[character_id, i] == null)
            {
                inventoryItems[character_id, i] = skinData;
                break;
            }
        }
    }
}
