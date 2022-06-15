using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSmoothMove : MonoBehaviour
{
    private Vector3 actualPos;
    private Animator anim;
    public void Start()
    {
        actualPos = new Vector3(0, 0, 0);
        anim = GetComponent<Animator>();
    }
    public void moveToPos(Vector3 pos)
    {
        actualPos = pos;
        anim.SetBool("move", true);
    }
    public void movePosFr()
    {
        gameObject.transform.localPosition = actualPos;
        anim.SetBool("move", false);
    }
}
