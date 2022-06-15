using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBuyBtn : MonoBehaviour
{
    [SerializeField] Animator myAnim;
    public void cancel()
    {
        myAnim.SetBool("breaking", false);
    }
}
