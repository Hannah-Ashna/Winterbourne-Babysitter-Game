using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggCR_Script : MonoBehaviour
{

    [Header("Egg Status")]
    [SerializeField] private GameObject thermometerHot;
    [SerializeField] private GameObject thermometerCold;
    [SerializeField] private GameObject thermometerNormal;
    [SerializeField] private GameObject eggDead;
    [SerializeField] private GameObject eggHealing;

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
    private bool isInRange;

    // Constants
    private string normalStatus = "This egg looks comfortable! Maybe I should check on the others. \n\n [Pearl Mussel: This species is Critically Endangered]";
    private string hotStatus = "This egg is overheating, I need to cool it down! \n\n[Press F to use a Fan]";
    private string coldStatus = "This egg is freezing, I need to warm it up immediately! \n\n[Press B to use a Blanket]";
    private string deadStatus = "We've lost this egg ... I'll have to break the news to the Mayor \n\n [Pearl Mussel: This species is Critically Endangered]";
    private string recoveryStatus = "This egg is recovering, I need to give it some time ... \n\n [Pearl Mussel: This species is Critically Endangered]";

    // Start is called before the first frame update
    void Start()
    {
        status = "Normal";
        dangerLevel = 0;
        thermometerHot.SetActive(false);
        thermometerCold.SetActive(false);
        thermometerNormal.SetActive(true);
        eggDead.SetActive(false);
        eggHealing.SetActive(false);

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

        if (currentTime < previousCheck)
        {
            previousCheck = currentTime;

            if (status == "Normal")
            {
                eventProbability = Random.Range(0, 20);

                if (eventProbability >= 12)
                {
                    dangerType = Random.Range(0, 5);
                    setWarningIcon(dangerType);
                }
            }
            else if (status == "Hot" || status == "Cold")
            {
                ++dangerLevel;

                // Check if the Egg has Died
                if (dangerLevel >= 2 && status != "Dead")
                {
                    status = "Dead";
                    thermometerHot.SetActive(false);
                    thermometerCold.SetActive(false);
                    thermometerNormal.SetActive(false);
                    eggDead.SetActive(true);
                    eggHealing.SetActive(false);

                    SceneUpdatesManagerScript.updateTotalEggs();
                    PlayerInventoryScript.updateCR();
                }
            }
        }

        if (isInRange) 
        {
            // Set Dialogue
            RunDialogue();

            // Interact with Object
            if (Input.GetKeyDown(KeyCode.B) && status == "Cold")
            {
                if (PlayerInventoryScript.getBlankets() > 0)
                {
                    PlayerInventoryScript.setBlankets(PlayerInventoryScript.getBlankets() - 1);
                    startRecovery(currentTime, "Blanket");
                }

            }
            else if (Input.GetKeyDown(KeyCode.F) && status == "Hot")
            {
                if (PlayerInventoryScript.getFans() > 0)
                {
                    PlayerInventoryScript.setFans(PlayerInventoryScript.getFans() - 1);
                    startRecovery(currentTime, "Fan");
                }

            }
        }
 
        if (status == "Recovery") 
        {
            if ((recoveryStartTime - currentTime) > 1)
            {
                status = "Normal";
                thermometerHot.SetActive(false);
                thermometerCold.SetActive(false);
                thermometerNormal.SetActive(true);
                eggDead.SetActive(false);
                eggHealing.SetActive(false);
            }
        }

    }

    private void setWarningIcon(int dangerType) {
        if (dangerType == 0) {
            status = "Cold";
            thermometerHot.SetActive(false);
            thermometerCold.SetActive(true);
            thermometerNormal.SetActive(false);
            eggDead.SetActive(false);
            eggHealing.SetActive(false);
            dangerLevel = 1;
        } else if (dangerType >= 1) {
            status = "Hot";
            thermometerHot.SetActive(true);
            thermometerCold.SetActive(false);
            thermometerNormal.SetActive(false);
            eggDead.SetActive(false);
            eggHealing.SetActive(false);
            dangerLevel = 1;
        }
    }

    public void startRecovery(int recoveryTime, string recoveryType) {
        status = "Recovery";
        dangerLevel = 0;
        recoveryStartTime = recoveryTime;
        recoveryItemType = recoveryType;

        thermometerHot.SetActive(false);
        thermometerCold.SetActive(false);
        thermometerNormal.SetActive(false);
        eggDead.SetActive(false);
        eggHealing.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            SceneUpdatesManagerScript.setDialogue("...");
        }
    }

    private void RunDialogue() {
        switch (status)
        {
            case "Normal":
                SceneUpdatesManagerScript.setDialogue(normalStatus);
                break;
            case "Hot":
                SceneUpdatesManagerScript.setDialogue(hotStatus);
                break;
            case "Cold":
                SceneUpdatesManagerScript.setDialogue(coldStatus);
                break;
            case "Recovery":
                SceneUpdatesManagerScript.setDialogue(recoveryStatus);
                break;
            case "Dead":
                SceneUpdatesManagerScript.setDialogue(deadStatus);
                break;
            default:
                break;
        }
    }
}
