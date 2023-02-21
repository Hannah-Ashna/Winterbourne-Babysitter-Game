using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawnerScript : MonoBehaviour
{

    int Eggs_CR;
    int Eggs_VU;
    int Eggs_LC;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEggCount(int countCR, int countVU, int countLC) {
        Eggs_CR = countCR;
        Eggs_VU = countVU;
        Eggs_LC = countLC;
    }
}
