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

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI inventoryFanText;
    [SerializeField] private TextMeshProUGUI inventoryBlanketText;

    private TextMeshProUGUI[] choicesText;

    private List<string> tags;
    private Story currentStory;
    private bool dialogueIsPlaying;

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

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
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

    public void MakeChoice(int choiceIndex) 
    { 
        currentStory.ChooseChoiceIndex(choiceIndex);
        currentStory.ObserveVariable("prep_duration", (variableName, newValue) =>
        {
            timerText.text = "NO DROUGHT:\n" + newValue;
            timerText.fontSize = 14;
        });

        currentStory.ObserveVariable("inventory_fans", (variableName, newValue) =>
        {
            inventoryFanText.text = newValue + " Fan(s)";
            inventoryFanText.fontSize = 15;
        });

        currentStory.ObserveVariable("inventory_blankets", (variableName, newValue) =>
        {
            inventoryBlanketText.text = newValue + " Blanket(s)";
            inventoryBlanketText.fontSize = 15;
        });
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
}