using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NormanArm : MonoBehaviour
{
    [SerializeField] Sprite defaultArm;
    [SerializeField] RuntimeAnimatorController defaultAnim;
    [SerializeField] Sprite goldenArm;
    [SerializeField] RuntimeAnimatorController goldAnim;
    [SerializeField] Sprite santaArm;
    [SerializeField] RuntimeAnimatorController santaAnim;
    [SerializeField] Sprite johnArm;
    [SerializeField] RuntimeAnimatorController johnAnim;
    [SerializeField] Animator player;
    [SerializeField] SpriteRenderer armImage;
    [SerializeField] Animator armAnim;
    [SerializeField] PlayerController cont;
   
    int previousInt = 0;
    void OnEnable()
    {
        if (player.GetInteger("costume") == 2 && previousInt != 2)
        {
            changeToJohn();
            previousInt = 2;
        }
        if (player.GetInteger("costume") == 3 && previousInt != 3)
        {
            changeToGold();
            previousInt = 3;
        }
        if (player.GetInteger("costume") == 1 && previousInt != 1)
        {
            changeToSanta();
            previousInt = 1;
        }
        if (player.GetInteger("costume") == 0 && previousInt != 0)
        {
            changeToDefault();
            previousInt = 0;
        }
    }
    public void changeToGold()
    {

        armImage.sprite = goldenArm;
        armAnim.runtimeAnimatorController = goldAnim;
    }
    public void changeToJohn()
    {

        armImage.sprite = johnArm;
        armAnim.runtimeAnimatorController = johnAnim;
    }
    public void changeToSanta()
    {

        armImage.sprite = santaArm;
        armAnim.runtimeAnimatorController = santaAnim;
    }
    public void changeToDefault()
    {
       
        armImage.sprite = defaultArm;
        armAnim.runtimeAnimatorController = defaultAnim;
    }
    public void disableShoot()
    {
    
        armAnim.SetBool("shoot", false);
    }

}