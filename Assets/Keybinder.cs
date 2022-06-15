using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Keybinder : MonoBehaviour
{
    [SerializeField] string control;
    private static readonly Dictionary<string, string> replaceScript = new Dictionary<string, string>
    {
        { "Alpha0", "0" },
        { "Alpha1", "1" },
        { "Alpha2", "2" },
        { "Alpha3", "3" },
        { "Alpha4", "4" },
        { "Alpha5", "5" },
        { "Alpha6", "6" },
        { "Alpha7", "7" },
        { "Alpha8", "8" },
        { "Alpha9", "9" },
        {"Exclaim", "!"},
        { "DoubleQuote", "\"" },
        {"Dollar", "$"},
        {"Percent", "%"},
        {"Ampersand", "&"},
        {"Quote", "'"},
        {"LeftParen", "("},
        {"RightParen", ")"},
        {"Asterik", "*"},
        {"Plus", "+"},
        {"Comma", ","},
        {"Minus", "-"},
        {"Period", "."},
        {"Slash", "/"},
        {"Colon", ":"},
        {"Semicolon", ";"},
        {"Less", "<"},
        {"Equals", "="},
        {"Greater", ">"},
        {"At", "@"},
        {"LeftBracket", "["},
        {"RightBracket", "]"},
        {"Backslash", "\\"},
        {"Caret", "^"},
        {"Underscore", "_"},
        {"BackQuote", "`"},
        {"LeftCurlyBracket", "{"},
        {"RightCurlyBracket", "}"},
        {"Tilde", "~"},
        {"Keypad0", "0"},
        {"Keypad1", "1"},
        {"Keypad2", "2"},
        {"Keypad3", "3"},
        {"Keypad4", "4"},
        {"Keypad5", "5"},
        {"Keypad6", "6"},
        {"Keypad7", "7"},
        {"Keypad8", "8"},
        {"Keypad9", "9"},
        {"KeypadPeriod", "."},
        {"KeypadDivide", "/"},
        {"KeypadMultiply", "*"},
        {"KeypadMinus", "-"},
        {"KeypadPlus", "+"},
        {"KeypadEnter", "ENTER"},
        {"KeypadEquals", "="},
        {"UpArrow", "UP"},
        {"DownArrow", "DOWN"},
        {"RightArrow", "RIGHT"},
        {"LeftArrow", "LEFT"},
        {"PageUp", "PGUP"},
        {"PageDown", "PGDOWN"},
        {"RightShift", "SHIFT" },
        {"LeftShift", "SHIFT"},
        {"RightControl", "CONT"},
        {"LeftControl", "CONT"},
        {"RightAlt", "ALT"},
        {"LeftAlt", "ALT"},
        {"RightCommand", "CMD"},
        {"LeftCommand", "CMD"},
        {"Mouse0", "LMB"},
        {"Mouse1", "RMB"},
        {"Mouse2", "CMB"},
        {"Backspace", "BACK.."}

    };
    [SerializeField] private Text myText;


     void Awake()
    {
       myText.text = "" + GameInputManager.checkKey(control);
       cleanseText();
    }
    public void updateText()
    {
        myText.text = "" + GameInputManager.checkKey(control);
        cleanseText();
    }
    public void waitForKeySet()
    {
        myText.text = "...";
        StartCoroutine(getInput());
    }
    IEnumerator getInput()
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                myText.text = "" + vKey;
                GameInputManager.swapKey(vKey, control);
            }
        }
        cleanseText();

    }
    private void cleanseText()
    {
        if (replaceScript.ContainsKey(myText.text))
        {
            myText.text = replaceScript[myText.text];
        }
        else
        {
            myText.text = myText.text.ToUpper();
        }
    }
}
