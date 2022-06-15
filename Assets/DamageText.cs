using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using System;
public class DamageText : MonoBehaviour
{
   [SerializeField] TMP_Text damageText;

    void Start()
    {
        setText(transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-20,20));
        StartCoroutine(death(5));
    }
    public void setText(double damage)
    {
        double size = 2 + damage / 10;
        damageText.text = "<size=" + size + ">" + Math.Round(damage) + "</size>";
    }
    private IEnumerator death(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NetworkServer.Destroy(gameObject);
    }
}
