using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JupArm : MonoBehaviour
{
    [SerializeField] Sprite[] defaultArm;
    [SerializeField] Sprite[] pirateArm;
    Sprite[] currentArm;
    [SerializeField] Animator player;
    [SerializeField] SpriteRenderer armImage;

    int previousInt = 0;
    void OnEnable()
    {
        checkWhichSkin();
    }
    private void checkWhichSkin()
    {
        if (player.GetCurrentAnimatorStateInfo(0).IsName("StartUp"))
        {
            StartCoroutine(checkAgain());
            return;
        }
        if (player.GetInteger("costume") == 3 && previousInt != 3)
        {
            changeToPirate();
            previousInt = 3;
        }
        if (player.GetInteger("costume") == 0 && previousInt != 0)
        {
            changeToDefault();
            previousInt = 0;
        }
    }
    public void changeToDefault()
    {
        armImage.sprite = defaultArm[5];
        currentArm = defaultArm;
    }
    public void changeToPirate()
    {
        armImage.sprite = pirateArm[5];
        currentArm = pirateArm;
    }
    public Sprite getArmSprite(int stage)
    {
        if(currentArm == null)
        {
            currentArm = defaultArm;
        }
        return currentArm[stage];
    }
    IEnumerator checkAgain()
    {
        yield return new WaitForSeconds(.2f);
        checkWhichSkin();
    } 
}
