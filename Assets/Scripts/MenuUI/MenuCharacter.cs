using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    Animator characterAnimator;
    RectTransform characterTransform;

    [SerializeField] CharacterSelect characterSelect;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        characterTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        updateAnimator();
    }

    public void updateAnimator()
    {
        characterAnimator.SetInteger("playerNum", CharacterSelect.characterSelected);
        characterAnimator.SetInteger("outfit_id", CharacterSelect.costumeSelected[CharacterSelect.characterSelected]);
        characterAnimator.CrossFade("PlayerMenuClear", 0f);
        characterTransform.sizeDelta = new Vector2(CharacterSelect.WINDTH_VALUES[CharacterSelect.characterSelected] * 2.5f, CharacterSelect.WINDTH_VALUES[CharacterSelect.characterSelected] * 2.5f);
    }
}
