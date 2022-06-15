using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tab : MonoBehaviour
{
    [SerializeField] TabManager tabManager;
    [SerializeField] GameObject content;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite selectedIconSprite;
    [SerializeField] Sprite unselectedSprite;
    [SerializeField] Sprite unselectedIconSprite;
    [SerializeField] Image icon;
    [SerializeField] bool startSelected;
    Image image;

    public bool selected { get; set; }

    Button button;
    
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        if (startSelected)
        {
            select();
        }
    }
    public void select()
    {
        tabManager.resetTabs();
        icon.sprite = selectedIconSprite;
        image.sprite = selectedSprite;
        button.interactable = false;
        selected = true;
        content.SetActive(true);
    }
    public void unselect()
    {
        icon.sprite = unselectedIconSprite;
        image.sprite = unselectedSprite;
        button.interactable = true;
        selected = false;
        content.SetActive(false);
    }
}
