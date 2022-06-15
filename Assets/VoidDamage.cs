using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDamage : MonoBehaviour
{
    [SerializeField] Void voidd;

    #if UNITY_SERVER || UNITY_EDITOR
    private void OnTriggerExit2D(Collider2D collision)
    {
        voidd.addPlayerToStorm(collision.gameObject.GetComponent<Health>());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        voidd.removePlayerFromStorm(collision.gameObject.GetComponent<Health>());
    }
    #endif
}
