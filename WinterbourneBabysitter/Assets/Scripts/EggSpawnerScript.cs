using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EggSpawnerScript : MonoBehaviour
{

    [Header("Spawn Locations")]
    [SerializeField] private GameObject[] location;

    [Header("Prefabs")]
    [SerializeField] public GameObject Eggs_CRPrefab;
    [SerializeField] public GameObject Eggs_VUPrefab;
    [SerializeField] public GameObject Eggs_LCPrefab;

    private bool[] locationTaken;

    GameObject[] Eggs_CR;
    GameObject[] Eggs_VU;
    GameObject[] Eggs_LC;


    // Start is called before the first frame update
    void Start()
    {
        // Setup Array
        locationTaken = new bool[location.Length]; 
        spawnEggs_LC();
        spawnEggs_VU();
        spawnEggs_CR();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEggCount(int countLC, int countVU, int countCR) { 
        Eggs_LC = new GameObject[countLC];
        Eggs_VU = new GameObject[countVU];
        Eggs_CR = new GameObject[countCR];
    }

    void spawnEggs_LC() {
        if (Eggs_LC.Length > 0) 
        {
            for (int i = 0; i < Eggs_LC.Length; i++) {
                locationTaken[i] = true;
                Eggs_LC[i] = Instantiate(Eggs_LCPrefab);
                Eggs_LC[i].transform.position = location[i].transform.position;
            }
        }
    }

    void spawnEggs_VU()
    {
        if (Eggs_VU.Length > 0)
        {
            for (int i = 0; i < Eggs_VU.Length ; i++)
            {
                locationTaken[i] = true;
                Eggs_VU[i] = Instantiate(Eggs_VUPrefab);
                Eggs_VU[i].transform.position = location[Eggs_LC.Length + i].transform.position;
            }
        }
    }

    void spawnEggs_CR()
    {
        if (Eggs_CR.Length > 0)
        {
            for (int i = 0; i < Eggs_CR.Length; i++)
            {
                locationTaken[i] = true;
                Eggs_CR[i] = Instantiate(Eggs_CRPrefab);
                Eggs_CR[i].transform.position = location[Eggs_LC.Length + Eggs_VU.Length + i].transform.position;
            }
        }
    }
}
