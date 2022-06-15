using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestroyPoppedBubble : MonoBehaviour
{
    public void killBubble()
    {
        Destroy(gameObject);
    }
}
