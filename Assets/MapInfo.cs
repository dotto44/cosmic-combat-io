using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapInfo : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPointCollections;
    //private static  string[] mapNames  = {"THEMOON", "LOSTLAB", "ICICLECAVERNS"};
    private static int[][] mapBounds = {
        new int[]{-150, 115, -50, 95},
        new int[]{-75, 70, 11, 80},
        new int[]{-75, 70, 11, 100},
        new int[]{-1000, 1000, -500, 500}
    };

     static readonly string[] scenes = { "Geometric", "LostLab", "IcicleCaverns", "Mars"};
     static readonly string[] mapNames = { "THE MOON", "SPACE BASE", "ICICLE CAVERNS", "MARS" };
    [SerializeField] Sprite[] icons;
    [SerializeField] Sprite[] backdrops;
     static readonly string[] modes = { "FREE-FOR-ALL" };

    [SerializeField] CustomNetworkManager customNetworkManager;
    public string whichMap = "Geometric"; 
    
    void Start()
    {
        toggleMap(whichMap);
    }
    public int[] getBoundaries()
    {
        return mapBounds[indexOfMap()];
    }
    public int indexOfMap()
    {
        for(int i = 0; i < scenes.Length; i++)
        {
            if (whichMap.Equals(scenes[i]))
            {
                return i;
            }
        }
        return -1;
    }
    public void toggleMap(string map)
    {
        whichMap = map;
        setMapSpawns();
        setMapScene();
    }
    public void setMapSpawns()
    {
        int spawnPoint = indexOfMap();
        for (int i = 0; i < spawnPointCollections.Length; i++)
        {
            if (i == spawnPoint)
            {
                spawnPointCollections[i].SetActive(true);
            }
            else
            {
                spawnPointCollections[i].SetActive(false);
            }
        }
    }
    public void setMapScene()
    {
        customNetworkManager.onlineScene = scenes[indexOfMap()];
    }
    public string getNRandomCombos(int n)
    {
        string comboString = "";

        ArrayList takenCombos = new ArrayList();
        while (takenCombos.Count < n)
        {
            int randomMap = Random.Range(0, mapNames.Length);
            int randomMode = Random.Range(0, modes.Length);
            string combo = randomMap + "," + randomMode + ",";
            if (!takenCombos.Contains(combo))
            {
                takenCombos.Add(combo);
            }
        }

        for(int i = 0; i < n; i++)
        {
            comboString += takenCombos[i];
        }

        return comboString;
    }
    public string getMapName(int n)
    {
        return mapNames[n];
    }
    public string getMode(int n)
    {
        return modes[n];
    }
    public Sprite getIcon(int n)
    {
        return icons[n];
    }
    public Sprite getBackdrop(int n)
    {
        return backdrops[n];
    }
}
