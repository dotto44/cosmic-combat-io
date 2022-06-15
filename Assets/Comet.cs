using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController[] comets;
    [SerializeField] Animator anim;
    [SerializeField] Animator mover;

    int randomNum;
    Vector3 resetPosition;
    void Start()
    {
        resetPosition = new Vector3(0, 302, 0);
        resetCometRandomTime();
    }
    void resetComet()
    {
        StartCoroutine(actuallyReset());
    }
    void resetCometRandomTime()
    {
        randomNum = Random.Range(0, 8);
        mover.SetFloat("speed", Random.Range(1f, 2.25f));
        mover.SetFloat("offset", Random.Range(0, 1));
        anim.runtimeAnimatorController = comets[randomNum];
        gameObject.transform.localPosition = resetPosition;
        gameObject.transform.parent.transform.localPosition = new Vector3(Random.Range(40, 500), 0, 0);
    }
     IEnumerator actuallyReset(){
        mover.enabled = false;
         yield return new WaitForSeconds(Random.Range(0, 2));
        mover.enabled = true;
         randomNum = Random.Range(0, 8);
        mover.SetFloat("speed", Random.Range(1f, 2.25f));
        anim.runtimeAnimatorController = comets[randomNum];
        gameObject.transform.localPosition = resetPosition;
        gameObject.transform.parent.transform.localPosition = new Vector3(Random.Range(80, 475), 0, 0);
        }
}
