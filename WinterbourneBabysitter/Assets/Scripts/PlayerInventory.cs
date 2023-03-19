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

    public int roundsSurvived;

    // Start is called before the first frame update
    void Start()
    {
        fans = 0;
        blankets = 0;
        roundsSurvived = 0;
    }

    public void setFans(int newValue) {
        fans = newValue;
    }

    public void setBlankets(int newValue) {
        blankets = newValue;
    }

    public int getFans()
    {
        return fans;
    }

    public int getBlankets()
    {
        return blankets;
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

    private void Awake()
    {
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

    public void increaseRoundsSurvived() {
        roundsSurvived++;
    }
}
