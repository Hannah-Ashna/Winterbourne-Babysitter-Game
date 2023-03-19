using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggLC_Script : MonoBehaviour
{

    [Header("Egg Status")]
    [SerializeField] private GameObject thermometerHot;
    [SerializeField] private GameObject thermometerCold;
    [SerializeField] private GameObject thermometerNormal;
    [SerializeField] private GameObject eggDead;

    private SceneUpdatesManager SceneUpdatesManagerScript;
    private PlayerInventory PlayerInventoryScript;

    private string status;
    private int currentTime;
    private int previousCheck;
    private int eventProbability;
    private int dangerType;
    private int dangerLevel;
    private int recoveryStartTime;
    private string recoveryItemType;

    // Start is called before the first frame update
    void Start()
    {
        status = "Normal";
        dangerLevel = 0;
        thermometerHot.SetActive(false);
        thermometerCold.SetActive(false);
        thermometerNormal.SetActive(true);

        // Get Updates Manager
        SceneUpdatesManagerScript = GameObject.FindObjectOfType<SceneUpdatesManager>().GetComponent<SceneUpdatesManager>();
        PlayerInventoryScript = GameObject.FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
        currentTime = SceneUpdatesManagerScript.getTime();
    }

    // Update is called once per frame
    void Update()
    {
        previousCheck = currentTime;
        currentTime = SceneUpdatesManagerScript.getTime();

        if (currentTime < previousCheck) {
            previousCheck = currentTime;

            if (status == "Normal")
            {
                eventProbability = Random.Range(0, 20);

                if (eventProbability >= 17)
                {
                    dangerType = Random.Range(0, 4);
                    setWarningIcon(dangerType);
                }
            }
            else if (status == "Recovery")
            {
                // Do Something Bout Recovery Maths
            }
            else 
            {
                ++dangerLevel;

                // Check if the Egg has Died
                if (dangerLevel >= 4 && status != "Dead") {
                    status = "Dead";
                    thermometerHot.SetActive(false);
                    thermometerCold.SetActive(false);
                    thermometerNormal.SetActive(false);
                    eggDead.SetActive(true);

                    SceneUpdatesManagerScript.updateTotalEggs();
                    PlayerInventoryScript.updateLC();
                }
            }
        }

    }

    private void setWarningIcon(int dangerType) {
        if (dangerType == 0) {
            status = "Cold";
            thermometerHot.SetActive(false);
            thermometerCold.SetActive(true);
            thermometerNormal.SetActive(false);
            dangerLevel = 1;
        } else if (dangerType >= 1) {
            status = "Hot";
            thermometerHot.SetActive(true);
            thermometerCold.SetActive(false);
            thermometerNormal.SetActive(false);
            dangerLevel = 1;
        } else {
            status = "Normal";
            thermometerHot.SetActive(false);
            thermometerCold.SetActive(false);
            thermometerNormal.SetActive(true);
            dangerLevel = 0;
        }
    }

    public void startRecovery(int recoveryTime, string recoveryType) {
        status = "Recovery";
        dangerLevel = 0;
        recoveryStartTime = recoveryTime;
        recoveryItemType = recoveryType;
    }
}
