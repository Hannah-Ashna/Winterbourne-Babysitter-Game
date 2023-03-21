using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingDialogueTrigger : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool DialogueInstance = false;

    private void Update()
    {
        if (!DialogueInstance)
        {
            LoopingDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            DialogueInstance = true;
        }
    }


}
