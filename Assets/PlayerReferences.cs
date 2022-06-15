using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] GameObject[] characters;

    public GameObject getCharacter(int index)
    {
        return characters[index];
    }
}
