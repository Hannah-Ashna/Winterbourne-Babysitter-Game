using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool DialogueInstance = false;

    private void Update()
    {
        if (!DialogueInstance)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            DialogueInstance = true;
        }
    }


}
