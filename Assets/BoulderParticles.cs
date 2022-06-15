using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderParticles : MonoBehaviour
{
    [SerializeField] Color[] boulderColors;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        var main = ps.main;
        Debug.Log((int)gameObject.transform.rotation.eulerAngles.x);
        main.startColor = boulderColors[(int)gameObject.transform.rotation.eulerAngles.x];
        
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 48);
    }

}
