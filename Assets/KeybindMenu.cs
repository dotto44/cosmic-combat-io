using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeybindMenu : MonoBehaviour
{
    [SerializeField] Keybinder[] keybinders;
    [SerializeField] UpdateAudio volumeSlider;
    void OnEnable()
    {
        updateAllKeyBinders();
    }
    public void updateAllKeyBinders()
    {
        volumeSlider.updateSliderGraphic();
        foreach (Keybinder k in keybinders)
        {
            k.updateText();
        }
    }
}
