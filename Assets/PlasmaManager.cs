using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlasmaManager : NetworkBehaviour
{
   
    [SerializeField] private Transform[] plasmaPoints = new Transform[] { };
    [SerializeField] int plasmaSize = 10;
    private List<int> plasmaSelects = new List<int> { };
    private int randomInt;
    private bool isUnique = false;

    public GameObject plasmaPrefab;
    public GameObject heartPrefab;

    public override void OnStartServer()
    {
        for (int x = 0; x < plasmaSize; x++)
        {
            //check for available locations, break out if taking too long
            int wonderWhich = Random.Range(0, 10);
            int pos = GetPossibleLocation();
            Spawn(wonderWhich, pos);
        }
    }
    private int GetPossibleLocation()
    {
        int safety = 0;
        do
        {
            safety++;
            randomInt = Random.Range(0, plasmaPoints.Length);
        } while (plasmaSelects.Contains(randomInt) && safety < 20);

        plasmaSelects.Add(randomInt);
        return randomInt;
    }
    private void Spawn(int wonderWhich, int pos)
    {
        if (wonderWhich <= 4)
        {
            //Spawn plasma pack

            var item = (GameObject)Instantiate(plasmaPrefab, plasmaPoints[pos].position, plasmaPoints[pos].rotation);
            NetworkServer.Spawn(item);
        }
        else
        {
            //spawn heart
            var enemy = (GameObject)Instantiate(heartPrefab, plasmaPoints[pos].position, plasmaPoints[pos].rotation);
            NetworkServer.Spawn(enemy);
        }
    }
    public void SpawnNewPlasmaPackMakeup()
    {
        int wonderWhich = Random.Range(0, 10);
        int pos = GetPossibleLocation();
        Spawn(wonderWhich, pos);
    }
    public void SpawnNewPlasmaPack(int pos)
    {
        int wonderWhich = Random.Range(0, 10);
        int safety = 0;

        int[] possibleValues = new int[plasmaPoints.Length - plasmaSelects.Count];
        int count = 0;
        Debug.Log("NUMBER OF FILLED SPOTS: " + plasmaSelects.Count);
        for(int i = 0; i < plasmaPoints.Length; i++)
        {
            if (!plasmaSelects.Contains(i))
            {
                possibleValues[count] = i;
                count++;
            }
        }
        plasmaSelects.Remove(pos);
        
        randomInt = Random.Range(0, possibleValues.Length);

        plasmaSelects.Add(possibleValues[randomInt]);
        if (wonderWhich <= 4)
        {
            var enemy = (GameObject)Instantiate(plasmaPrefab, plasmaPoints[possibleValues[randomInt]].position, plasmaPoints[possibleValues[randomInt]].rotation);
            //enemy.transform.GetChild(0).GetComponent<PlasmaPack>().positionNum = possibleValues[randomInt];
           
            //enemy.transform.parent = null;
            NetworkServer.Spawn(enemy);
        }
        else
        {
            var enemy = (GameObject)Instantiate(heartPrefab, plasmaPoints[possibleValues[randomInt]].position, plasmaPoints[possibleValues[randomInt]].rotation);
            //enemy.transform.GetChild(0).GetComponent<HeartPack>().pos = possibleValues[randomInt];
           
            //enemy.transform.parent = null;
            NetworkServer.Spawn(enemy);
        }
        if(plasmaSelects.Count < plasmaSize - 2)
        {
            SpawnNewPlasmaPackMakeup();
        }
    }
}
