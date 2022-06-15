using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
   [SerializeField] GameObject[] windows;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void turnOn()
    {
        anim.SetBool("onScreen", true);
    }
    public void turnOff()
    {
        anim.SetBool("onScreen", false);
        anim.SetBool("clear", true);
    }
    public void disableAllWindows()
    {
        anim.SetBool("clear", false);
        foreach (GameObject obj in windows)
        {
            obj.SetActive(false);
        }
    }
}
