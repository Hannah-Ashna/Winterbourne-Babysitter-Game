using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUpdatesManager : MonoBehaviour
{

    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI fans;
    [SerializeField] private TextMeshProUGUI blankets;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Egg Spawner")]
    [SerializeField] private GameObject eggSpawner;

    [Header("Player Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogue;

    private PlayerInventory inventory;
    private EggSpawnerScript spawner;

    float currentTime;
    int displayTime;
    public float startingTime = 225f;
    private int totalEggs;

    // Start is called before the first frame update
    void Start()
    {
        // Start the Countdown!
        currentTime = startingTime;
        setDialogue("Alright ... let's do this!");

        try
        {
            // Setup Inventory Script
            inventory = GameObject.FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();

            fans.text = inventory.getFans().ToString() + " Fan(s)";
            blankets.text = inventory.getBlankets().ToString() + " Blanket(s)";

            // Set Drought Duration
            timerText.text = timerText.text = "DROUGHT:\n" + 15;

            // Set Number of Eggs to Spawn
            spawner = eggSpawner.GetComponent<EggSpawnerScript>();

            // Need to make more modular???
            spawner.setEggCount(5, 3, 2);
            inventory.setVillageEggStatus(5, 3, 2);
            totalEggs = 10;
        }
        catch {

        }

    }

    // Update is called once per frame
    void Update()
    { 
        // Handle Timer
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            if (totalEggs > 0)
            {
                // Continue Game
                setDialogue("Looks like we survived the drought! Time to report back to Mr. Trutta.");
                inventory.increaseRoundsSurvived();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            else 
            {
                setDialogue("Oh no ... they're all gone!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else {
            displayTime = (int)currentTime/15;
            timerText.text = timerText.text = "DROUGHT:\n" + displayTime.ToString();
        }

        try {
            fans.text = inventory.getFans().ToString() + " Fan(s)";
            blankets.text = inventory.getBlankets().ToString() + " Blanket(s)";
        } catch { }
    }

    public int getTime() {
        return displayTime;
    }

    public void updateTotalEggs() {
        totalEggs--;
    }

    public void setDialogue(string dialogueText) {
        dialogue.text= dialogueText;
    }
}
