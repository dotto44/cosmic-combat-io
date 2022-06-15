using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUIBar : MonoBehaviour
{
public Animator shopAnim;
    [SerializeField] DeathUI deathUI;
    [SerializeField] GameObject statMenu;
    [SerializeField] GameObject charMenu;
    bool toggle = false;
    private int from = 0;
    public void setHidden(){
        shopAnim.SetBool ("hidden", true);
    }
    public void setVisible(){
        shopAnim.SetBool ("hidden", false);
    }
    public void quickToggle(int from)
    {
        shopAnim.SetBool("swap", true);
        this.from = from;
    }
    public void performTheActualToggle()
    {
        Debug.Log("HERE");
        shopAnim.SetBool("swap", false);
        if (from == 1)
        {
            charMenu.SetActive(false);
            statMenu.SetActive(true);
        }
        if(from == 0)
        {
            statMenu.SetActive(false);
            charMenu.SetActive(true);
        }
    }
    public void setVisibleAndHidden(){
        if (toggle) {
            toggle = false;
            shopAnim.SetBool ("hidden", true);
        } else {
            toggle = true;
            shopAnim.SetBool ("hidden", false);
        }

    }
}
