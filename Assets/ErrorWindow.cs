using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ErrorWindow : MonoBehaviour
{
    [SerializeField] Text errorMessage;
  
    public void turnOffWindow()
    {
        gameObject.SetActive(false);
    }
    public void sendErrorMessage(string message)
    {
        errorMessage.text = message;
        gameObject.SetActive(true);
    }
}
