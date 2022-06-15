using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefaultCostume : MonoBehaviour
{
    [SerializeField] Sprite[] defaultCostumes;
    [SerializeField] Image costume;
    [SerializeField] RectTransform costumeT;
    [SerializeField] CharacterSelect characterSelect;

    public void setDefault(int num)
    {
        costume.sprite = defaultCostumes[num];
        costumeT.sizeDelta = new Vector2(SkinInventory.OUTFIT_WINDTH_VALUES[num], costumeT.sizeDelta.y);
    }
    public void pressed()
    {
        characterSelect.setCostume(0, transform);
    }
}
