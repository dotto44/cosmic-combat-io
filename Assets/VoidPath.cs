using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPath : MonoBehaviour
{
    [SerializeField] Transform[] positions;

    public Transform[] getPositions()
    {
        return positions;
    }
}
