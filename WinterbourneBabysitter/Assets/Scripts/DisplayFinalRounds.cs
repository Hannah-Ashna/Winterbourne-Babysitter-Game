using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayFinalRounds : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI roundsSurvived;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInventory playerInventoryScript = GameObject.FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
        roundsSurvived.text = "Rounds Survived: " + playerInventoryScript.getRoundsSurvived();
    }
}
