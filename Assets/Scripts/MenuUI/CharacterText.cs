using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterText : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void setText()
    {
        string description = CharacterSelect.costumeSelected[CharacterSelect.characterSelected] == 0 ?
            SkinInventory.CHARACTER_DESCRIPTIONS[CharacterSelect.characterSelected] :
            SkinInventory.OUTFIT_DESCRIPTIONS[CharacterSelect.costumeSelected[CharacterSelect.characterSelected]];

        string newText = "<b><size=100>" + CharacterSelect.CHARACTER_NAMES[CharacterSelect.characterSelected] + "</b></size>\n"
            + "<size=80>" + description + "</size>";
        text.text = newText;

    }
}
