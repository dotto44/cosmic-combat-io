using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class BackgroundScroll : NetworkBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform stars2;
    [SerializeField] Transform stars3;
    [SerializeField] Transform stars5;
    Vector3 temp;
    Vector3 last;
    Vector3 first;
    [SerializeField] double amt = 1.5;
    bool firstTime = false;
    // Update is called once per frame
    [SerializeField] float lerpRate = 15;
    void Start()
    {
        last = cameraTransform.position;
        //gameObject.transform.position = cameraTransform.position;
    }
    void Update()
    {
        updateBacground();

        // }
    }
    void updateBacground()
    {
        if (isServer)
        {
            return;
        }
        temp = gameObject.transform.position;
        temp.x += (float)((cameraTransform.position.x - last.x) * amt);
        temp.y += (float)((cameraTransform.position.y - last.y) * amt);
        gameObject.transform.position = temp;
        last = cameraTransform.position;
        first = gameObject.transform.position;
    }
   
}
