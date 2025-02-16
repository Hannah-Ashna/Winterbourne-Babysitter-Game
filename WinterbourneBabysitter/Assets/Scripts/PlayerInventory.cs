using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int fans;
    public int blankets;

    public int totalEggs_CR;
    public int totalEggs_VU;
    public int totalEggs_LC;
    public int totalHelpers;

    public int roundsSurvived;
    public int previousRound;
    public int nonDroughtDays;

    public bool winConditionActive;
    public bool allExtinct;
    public int finalRounds;

    private int[] parameters;
    private bool informedEggs_CR;
    private bool informedEggs_VU;
    private bool informedEggs_LC;

    // Start is called before the first frame update
    void Start()
    {
        fans = 0;
        blankets = 0;
        roundsSurvived = 0;
        previousRound = 0;
        nonDroughtDays = 21;
        informedEggs_CR= false;
        informedEggs_VU= false;
        informedEggs_LC= false;
        parameters = new int[4];
    }

    public void setFans(int newValue) {
        fans = newValue;
    }

    public void setBlankets(int newValue) {
        blankets = newValue;
    }

    public void setTotalHelpers(int newValue) { 
        totalHelpers = newValue;
    }

    public void setWinConditionActive(bool newValue) { 
        winConditionActive = newValue;
    }

    public void setFinalRounds(int newValue)
    {
        finalRounds = newValue;
    }

    public int getFans() {
        return fans;
    }

    public int getBlankets() {
        return blankets;
    }

    public int getTotalHelpers() {
        return totalHelpers;
    }

    public int getPrepDuration() {
        return nonDroughtDays;
    }

    public int getRoundsSurvived()
    {
        return roundsSurvived;
    }
    public int getFinalRounds()
    {
        return finalRounds;
    }

    public string useFans() {
        if (fans > 0)
        {
            fans--;
            return "There we go, looks like it's already cooling down ...";
        }
        else {
            return "We've run out of fans!";
        }
    }

    public string useBlankets() {
        if (blankets > 0)
        {
            blankets--;
            return "Hopefully it's a bit warmer now ...";
        }
        else {
            return "We've run out of blankets!";
        }
    }

    private void Awake(){
        DontDestroyOnLoad(this);
    }

    public void setVillageEggStatus(int Eggs_LC, int Eggs_VU, int Eggs_CR) {
        totalEggs_LC= Eggs_LC;
        totalEggs_VU= Eggs_VU;
        totalEggs_CR= Eggs_CR;
    }

    public void updateLC() {
        totalEggs_LC--;
    }

    public void updateVU() {
        totalEggs_VU--;
    }

    public void updateCR() {
        totalEggs_CR--;
    }

    public bool isEggLCExtinct() {
        if (totalEggs_LC == 0 && informedEggs_LC == false) { informedEggs_LC = true; return true; } else { return false; }
    }

    public bool isEggVUExtinct() {
        if (totalEggs_VU == 0 && informedEggs_VU == false) { informedEggs_VU = true; return true; } else { return false; }
    }

    public bool isEggCRExtinct() {
        if (totalEggs_CR == 0 && informedEggs_CR == false) { informedEggs_CR = true; return true; } else { return false; }
    }

    public bool isAllExtinct() {
        if (informedEggs_LC && informedEggs_VU && informedEggs_CR) { return true; } else { return false; }
    }

    public bool getWinConditionActive() {
        return winConditionActive;
    }

    public void increaseRoundsSurvived() {
        previousRound = roundsSurvived;
        roundsSurvived++;
    }

    public int[] getNurseryParameters() {

        switch (roundsSurvived) 
        {
            case int rounds when (rounds == 0):
                parameters[0] = 3;
                parameters[1] = 1;
                parameters[2] = 1;
                parameters[3] = 120;

                nonDroughtDays = 18;
                return parameters;

            case int rounds when (rounds >= 1 && rounds <= 3):
                if (totalEggs_LC > 0) { parameters[0] = Random.Range(3, 6); } else { parameters[0] = 0; }
                if (totalEggs_VU > 0) { parameters[1] = Random.Range(1, 4); } else { parameters[1] = 0; }
                if (totalEggs_CR > 0) { parameters[2] = Random.Range(1, 3); } else { parameters[2] = 0; }

                parameters[3] = Random.Range(120, 150);
                nonDroughtDays = Random.Range(12, 15);
                return parameters;

            case int rounds when (rounds >= 4 && rounds <= 8):
                if (totalEggs_LC > 0) { parameters[0] = Random.Range(3, 6); } else { parameters[0] = 0; }
                if (totalEggs_VU > 0) { parameters[1] = Random.Range(1, 4); } else { parameters[1] = 0; }
                if (totalEggs_CR > 0) { parameters[2] = Random.Range(1, 3); } else { parameters[2] = 0; }

                parameters[3] = Random.Range(150, 255);
                nonDroughtDays = Random.Range(6, 12);
                return parameters;

            case int rounds when (rounds >= 9):
                if (totalEggs_LC > 0) { parameters[0] = Random.Range(3, 6); } else { parameters[0] = 0; }
                if (totalEggs_VU > 0) { parameters[1] = Random.Range(1, 4); } else { parameters[1] = 0; }
                if (totalEggs_CR > 0) { parameters[2] = Random.Range(1, 3); } else { parameters[2] = 0; }

                parameters[3] = Random.Range(255, 300);
                nonDroughtDays = 8;
                return parameters;

            default:
                parameters[0] = 5;
                parameters[1] = 0;
                parameters[2] = 0;
                parameters[3] = 5;
                nonDroughtDays = 18;
                return parameters;
        }
    }
}
