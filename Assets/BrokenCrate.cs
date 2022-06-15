using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCrate : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb2D;
    public float thrust = 2.0f;
    public float xDirection = 0.0f;
    public float yDirection = 1.0f;
    void Start()
    {
        rb2D.AddForce(new Vector2(xDirection, yDirection).normalized * thrust, ForceMode2D.Impulse);
    }
}
