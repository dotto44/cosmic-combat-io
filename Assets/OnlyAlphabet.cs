using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyAlphabet : MonoBehaviour
{
    public InputField mainInputField;

    public void Start()
    {
        // Sets the MyValidate method to invoke after the input field's default input validation invoke (default validation happens every time a character is entered into the text field.)
        mainInputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
    }

    private char MyValidate(char charToValidate)
    {
      
        if (!((int)(charToValidate) > 64 && (int)(charToValidate) < 91 ||  (int)(charToValidate) > 96 && (int)(charToValidate) < 123 || (int)(charToValidate) > 47 && (int)(charToValidate) < 58 || (int)(charToValidate) == 32))
        { 
            charToValidate = '\0';
        }
        if((int)(charToValidate) > 96 && (int)(charToValidate) < 123)
        {
            charToValidate = (char)((int)(charToValidate) - 32);
        }
        return charToValidate;
    }
}
