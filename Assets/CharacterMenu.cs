using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField] RectTransform moveToFront;
    [SerializeField] Animator stardustBox;
    void OnEnable ()
    {

       stardustBox.SetBool("characterMenuUp", true);
        MoveInHierarchy(1);
    }
    void OnDisable()
    {
        stardustBox.SetBool("characterMenuUp", false);
        MoveInHierarchy(-1);
    }
    public void MoveInHierarchy(int delta)
    {
        int index = moveToFront.GetSiblingIndex();
        moveToFront.SetSiblingIndex(index + delta);
    }
}
