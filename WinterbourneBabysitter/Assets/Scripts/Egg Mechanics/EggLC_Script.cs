using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggLC_Script : MonoBehaviour
{

    [Header("Thermometer")]
    [SerializeField] private GameObject thermometerHot;
    [SerializeField] private GameObject thermometerCold;
    [SerializeField] private GameObject thermometerNormal;

    private string status;
    // Start is called before the first frame update
    void Start()
    {
        status = "Normal";
        thermometerHot.SetActive(false);
        thermometerCold.SetActive(false);
        thermometerNormal.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
