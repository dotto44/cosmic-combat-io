using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteMovement : MonoBehaviour
{
    [SerializeField] float upperLimit;
    [SerializeField] float lowerLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float leftLimit;

    [SerializeField] float speed;
    [SerializeField] float minDistanceForNextPoint;
    [SerializeField] float maxDistanceForNextPoint;

    Vector2 target;

    float rotationSpeed = 5f;
    Quaternion from;
    Quaternion to;

    void Start()
    {
        transform.position = findTarget(); //random start location
        target = findTarget(); //new target to move to
        to = findRotationTarget();
    }

    void Update()
    {

        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, step);
        if(Vector2.Distance(target, transform.localPosition) < 3)
        {
            target = findTarget();
        }
        setRotation();
    }
    private void setRotation()
    {
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, step);
       // Debug.Log("MY ROT VS TARGET: " + transform.rotation.eulerAngles.z + "    " + to.eulerAngles.z);
        if (Mathf.Abs(to.eulerAngles.z - transform.rotation.eulerAngles.z) < 5)
        {
            from = to;
            to = findRotationTarget();
        //    Debug.Log("FROM: " + from.eulerAngles.z);
        //    Debug.Log("TO: " + from.eulerAngles.z);
        }
       
    }
    private Vector2 findTarget()
    {

        float yPoint = findPointY();
        float xPoint = findPointX();
        return new Vector2(xPoint, yPoint);
    }
    private Quaternion findRotationTarget()
    {

        float z = Random.Range(-180, 180);
       
        Quaternion returnQuat = Quaternion.Euler(0, 0, z);
       
        return returnQuat;
    }
    private float findPointX()
    {
        float xPoint = transform.localPosition.x;
        int safety = 0;
        while (Mathf.Abs(xPoint - transform.localPosition.x) < minDistanceForNextPoint && safety < 10)
        {
            xPoint = Random.Range(transform.localPosition.x - maxDistanceForNextPoint, transform.localPosition.x + maxDistanceForNextPoint);
            safety++;
        }
        if (xPoint > rightLimit)
        {
            xPoint = rightLimit;
        }
        if (xPoint < leftLimit)
        {
            xPoint = leftLimit;
        }
        return xPoint;
    }
    private float findPointY()
    {
        float xPoint = transform.localPosition.y;
        int safety = 0;
        while (Mathf.Abs(xPoint - transform.localPosition.y) < minDistanceForNextPoint && safety < 10)
        {
            xPoint = Random.Range(transform.localPosition.y - maxDistanceForNextPoint, transform.localPosition.y + maxDistanceForNextPoint);
            safety++;
        }
       
        if (xPoint > upperLimit)
        {
            xPoint = upperLimit;
        }
        if (xPoint < lowerLimit)
        {
            xPoint = lowerLimit;
        }
       
        return xPoint;
    }
}
