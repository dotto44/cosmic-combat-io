using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IciclePlayerCollision : MonoBehaviour
{
    [SerializeField] Icicle icicle;
    public void isNotServer()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        icicle.handleTrigger(collision);
    }
}
