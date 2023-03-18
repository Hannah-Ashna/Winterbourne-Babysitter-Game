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

    // Start is called before the first frame update
    void Start()
    {
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
            spawner.setEggCount(1, 2, 2);
        }
        catch {

        }

    }

    // Update is called once per frame
    void Update()
    {
        try {
            fans.text = inventory.getFans().ToString() + " Fan(s)";
            blankets.text = inventory.getBlankets().ToString() + " Blanket(s)";
        } catch { }
    }
}
