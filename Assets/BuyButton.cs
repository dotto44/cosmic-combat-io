using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] Animator myAnim;
     public void setBuyFalse()
    {
        myAnim.SetBool("buy", false);
        myAnim.SetBool("breaking", false);
    }
}
