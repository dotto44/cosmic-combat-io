using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public enum Rarity
    {
        Default,
        Green,
        Purple,
        Gold,
    }
    public Rarity rarity;
    public int outfit_id;
}
