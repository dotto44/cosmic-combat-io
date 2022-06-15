using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsBlocker : MonoBehaviour
{
    [SerializeField] NewsManager newsManager;
    [SerializeField] Animator myAnim;
    // Update is called once per frame
    void updateContentDisableFade()
    {
        newsManager.makeTheSwitch();
        myAnim.SetBool("fade", false);
    }
}
