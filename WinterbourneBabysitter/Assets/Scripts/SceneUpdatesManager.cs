using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneUpdatesManager : MonoBehaviour
{

    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI fans;
    [SerializeField] private TextMeshProUGUI blankets;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Egg Spawner")]
    [SerializeField] private GameObject eggSpawner;

    private PlayerInventory inventory;
    private EggSpawnerScript spawner;

    float currentTime;
    int displayTime;
    public float startingTime = 225f; 

    // Start is called before the first frame update
    void Start()
    {
        // Start the Countdown!
        currentTime = startingTime;

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
            spawner.setEggCount(3, 2, 5);
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
            currentTime = 0;
            Debug.Log("QUIT");
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
}
