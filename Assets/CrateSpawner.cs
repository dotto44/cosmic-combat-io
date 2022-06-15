
using Mirror;
using UnityEngine;
using System.Collections.Generic;

public class CrateSpawner : NetworkBehaviour
{
    [SerializeField] List<GameObject> crates;
    [SerializeField] double[] probability;

    int howMany;
    override public void OnStartServer()
    {
        base.OnStartServer();

        howMany = numberOfCrates();

        while(crates.Count > howMany)
        {
            int unluckyCrate = Random.Range(0, crates.Count);

            GameObject crate = crates[unluckyCrate];

            crates.RemoveAt(unluckyCrate);

            NetworkServer.Destroy(crate);
        }

    }
    public int numberOfCrates()
    {
        float total = 0;

        for (int i = 0; i < probability.Length; i++)
        {
            total += (float)probability[i];
        }
        float index = Random.Range(0.0f, total);

        double rollingTotal = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            rollingTotal += probability[i];
            if (index < rollingTotal)
            {
                Debug.Log("How Many: " + i);
                return i;
            }
        }
        Debug.Log("Total: " + total);
        Debug.Log("RollingTotal: " + rollingTotal);
        return 0;
    }

}
