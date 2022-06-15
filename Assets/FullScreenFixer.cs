using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenFixer : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        if(Screen.fullScreen){
            Screen.fullScreen = false;
        }
    }

}
