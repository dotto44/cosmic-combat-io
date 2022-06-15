using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelect : MonoBehaviour
{
    public const int NUMBER_OF_CHARACTERS = 5; 

    public static int characterSelected = 0;
    public static int[] costumeSelected = new int[NUMBER_OF_CHARACTERS];

    public static int[] WINDTH_VALUES = { 141, 117, 188, 317, 212, 288 };
    
    public static string[] CHARACTER_NAMES = { "NORM", "MARTIN", "WILLOW", "JACK", "CORAL", "PETER" };

    

    [SerializeField] GameObject[] characterButtons;
    [SerializeField] GameObject whiteFrame;
    [SerializeField] GameObject outfitWhiteFrame;
    [SerializeField] DefaultCostume defaultCostume;
    [SerializeField] SkinInventory skinInventory;
    [SerializeField] MenuCharacter shopCharacter;
    [SerializeField] CharacterText characterText;

    private void Start()
    {
        for(int i = 0; i < characterButtons.Length; i++)
        {
            int copy = i;
            characterButtons[copy].GetComponent<Button>().onClick.AddListener(delegate { setPosition(characterButtons[copy].transform, copy); });
        }
        
        skinInventory.addSkinToInventory(0, new InventoryItemData { outfit_id = 7, rarity = InventoryItemData.Rarity.Purple });
        skinInventory.addSkinToInventory(0, new InventoryItemData { outfit_id = 2, rarity = InventoryItemData.Rarity.Gold });

        skinInventory.addSkinToInventory(2, new InventoryItemData { outfit_id = 3, rarity = InventoryItemData.Rarity.Purple });

        skinInventory.addSkinToInventory(3, new InventoryItemData { outfit_id = 4, rarity = InventoryItemData.Rarity.Gold });

        skinInventory.addSkinToInventory(1, new InventoryItemData { outfit_id = 5, rarity = InventoryItemData.Rarity.Green });
        skinInventory.addSkinToInventory(1, new InventoryItemData { outfit_id = 6, rarity = InventoryItemData.Rarity.Purple });
    }
    
    public void setPosition(Transform parent, int num)
    {
        whiteFrame.transform.SetParent(parent);
        defaultCostume.setDefault(num);
    }

    public void setCharacterSelected(int characterSelected)
    {
        CharacterSelect.characterSelected = characterSelected;
        StatsHolder.characterSelected = characterSelected;
        skinInventory.updateInventory();
        shopCharacter.updateAnimator();
        characterText.setText();
        outfitWhiteFrame.transform.SetParent(skinInventory.findSelectedCostume());
    }
    public void setCostume(int costumeSelected, Transform framePos)
    {
        CharacterSelect.costumeSelected[CharacterSelect.characterSelected] = costumeSelected;
        shopCharacter.updateAnimator();
        characterText.setText();
        outfitWhiteFrame.transform.SetParent(framePos);
    }

}
