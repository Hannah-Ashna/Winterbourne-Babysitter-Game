using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image mayorImage;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Inventory UI")]
    [SerializeField] private GameObject playerInventory;
    [SerializeField] private TextMeshProUGUI inventoryFanText;
    [SerializeField] private TextMeshProUGUI inventoryBlanketText;

    private TextMeshProUGUI[] choicesText;

    private List<string> tags;
    private Story currentStory;
    private bool dialogueIsPlaying;
    private string typewriterText;
    private string savedJson;
    private PlayerInventory playerInventoryScript;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Dialogue Manager has more than one instance in current scene!");
        }

        instance = this;

        // Add button listener
        continueButton.onClick.AddListener(ContinueStory);
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        
        // Setup Inventory Script
        playerInventoryScript = playerInventory.GetComponent<PlayerInventory>();

        // Get the text for all the choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choices[index].gameObject.SetActive(false);
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        mayorImage.gameObject.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        mayorImage.gameObject.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            StopCoroutine("TypewriterText");
            typewriterText = currentStory.Continue();
            StartCoroutine("TypewriterText");
            DisplayChoices();
        }
        else
        {
            Debug.Log("Nothing left in the story");
            ExitDialogueMode();
        }
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) 
        {
            Debug.LogError("Too many choices than UI can support!");
        }

        if (currentChoices.Count > 0) 
        {
            continueButton.gameObject.SetActive(false);
        }

        if (currentChoices.Count == 0)
        {
            UpdateStoryVariables();
            continueButton.gameObject.SetActive(true);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }


    private IEnumerator SelectFirstChoice()
    {
        // Set the first choice
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    private IEnumerator TypewriterText()
    {
        dialogueText.text = "";

        foreach (char c in typewriterText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void MakeChoice(int choiceIndex) 
    { 
        currentStory.ChooseChoiceIndex(choiceIndex);
        UpdateStoryVariables();
        ContinueStory();
    }

    private void ParseTags()
    {

        tags = currentStory.currentTags;
        if (tags.Count > 0)
        {
            Debug.Log("hmm");
        }
    }

    private void UpdateStoryVariables() 
    {
        currentStory.ObserveVariable("prep_duration", (variableName, newValue) =>
        {
            timerText.text = "NO DROUGHT:\n" + newValue;
            timerText.fontSize = 14;
        });

        currentStory.ObserveVariable("inventory_fans", (variableName, newValue) =>
        {
            inventoryFanText.text = newValue + " Fan(s)";
            inventoryFanText.fontSize = 15;
            playerInventoryScript.setFans((int)newValue);
        });

        currentStory.ObserveVariable("inventory_blankets", (variableName, newValue) =>
        {
            inventoryBlanketText.text = newValue + " Blanket(s)";
            inventoryBlanketText.fontSize = 15;
            playerInventoryScript.setBlankets((int)newValue);
        });

        currentStory.ObserveVariable("show_mayor", (variableName, newValue) =>
        {
            if ((bool)newValue)
            {
                mayorImage.gameObject.SetActive(true);
            }
            else {
                mayorImage.gameObject.SetActive(false);
                savedJson = currentStory.state.ToJson();
                
            }
        });
    }
}
