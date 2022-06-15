using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NameInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GetName.userName != "" && GetName.userName != "Player")
        {
            GetComponent<InputField>().text = GetName.userName;
        }
    }

}
