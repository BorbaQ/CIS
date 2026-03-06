using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;
    public Image characterImage;
    public GameObject dialoguePanel;

    [Header("Choice UI")]
    public GameObject choiceButtonPrefab;
    public Transform choiceContainer;

    public bool inrange;
    public bool inTalk;

    public playerController player;

    private DialogueLineSO currentLine;

    private int selectedChoiceIndex = 0;
    private Button[] activeChoiceButtons;
    private float inputBlockTimer = 0f;

    void Update()
    {
        if (!inTalk) return;

        if (inputBlockTimer > 0)
        {
            inputBlockTimer -= Time.deltaTime;
            return;
        }

        if (currentLine != null && currentLine.choices != null && currentLine.choices.Length > 0)
        {
            HandleChoiceInput();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueLineSO startLine)
    {
        dialoguePanel.SetActive(true);
        inTalk = true;

        inputBlockTimer = 0.2f; // prevents skipping first line

        ShowLine(startLine);
    }


    void ShowLine(DialogueLineSO line)
    {
        currentLine = line;

        if (line == null)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = line.text;

        if (line.characterSprite != null)
            characterImage.sprite = line.characterSprite;

        ClearChoices();

        if (!string.IsNullOrEmpty(line.eventName))
        {
            TriggerEvent(line.eventName);
        }

        if (line.choices != null && line.choices.Length > 0)
        {
            ShowChoices(line);
        }
    }


    void NextLine()
    {
        if (currentLine.nextLine != null)
        {
            ShowLine(currentLine.nextLine);
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowChoices(DialogueLineSO line)
    {
        activeChoiceButtons = new Button[line.choices.Length];

        for (int i = 0; i < line.choices.Length; i++)
        {
            DialogueChoiceSO choice = line.choices[i];

            GameObject btn = Instantiate(choiceButtonPrefab, choiceContainer);

            TMP_Text txt = btn.GetComponentInChildren<TMP_Text>();
            txt.text = choice.choiceText;

            Button button = btn.GetComponent<Button>();

            int index = i;

            button.onClick.AddListener(() =>
            {
                inputBlockTimer = 0.15f;
                ShowLine(choice.nextLine);
            });

            activeChoiceButtons[i] = button;
        }

        selectedChoiceIndex = 0;
        HighlightChoice();
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    void TriggerEvent(string eventName)
    {
        Debug.Log("Dialogue Event Triggered: " + eventName);

        // Example event system
        switch (eventName)
        {
            case "KnockoutPlayer":
                Debug.Log("Player knocked out");
                break;

            case "GiveWarCrime":
                Debug.Log("War crimes achieved");
                break;
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        characterImage.sprite = null;

        ClearChoices();

        inTalk = false;

        if (player != null)
            player.cameraLock = false;
    }
    void HighlightChoice()
    {
        for (int i = 0; i < activeChoiceButtons.Length; i++)
        {
            TMP_Text txt = activeChoiceButtons[i].GetComponentInChildren<TMP_Text>();

            if (i == selectedChoiceIndex)
                txt.color = Color.yellow;
            else
                txt.color = Color.white;
        }
    }

    void HandleChoiceInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedChoiceIndex--;
            if (selectedChoiceIndex < 0)
                selectedChoiceIndex = activeChoiceButtons.Length - 1;

            HighlightChoice();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedChoiceIndex++;
            if (selectedChoiceIndex >= activeChoiceButtons.Length)
                selectedChoiceIndex = 0;

            HighlightChoice();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            activeChoiceButtons[selectedChoiceIndex].onClick.Invoke();
        }
    }

    
}