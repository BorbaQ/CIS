using UnityEngine;
using UnityEngine.UI;
using TMPro; // Important for TextMeshPro

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;  // Changed from Text to TMP_Text
    public Image characterImage;
    public GameObject dialoguePanel;
    public bool inrange;
    public bool inTalk;

    [Header("Dialogue Data")]
    public DialogueLine[] dialogueLines;

    public playerController player;

    public int currentLine = 0;

    void Start()
    {
    }

    void Update()
    {
        // Progress dialogue on Space or left mouse click
        if (inrange && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            NextLine();
        }
    }

    void ShowLine(int index)
    {
        if (index >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines[index];

        dialogueText.text = line.text; // TMP_Text works the same way
        if (line.characterSprite != null)
            characterImage.sprite = line.characterSprite;
    }

    public void NextLine()
    {
        ShowLine(currentLine);
        currentLine++;
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        characterImage.sprite = null;
        Debug.Log("Dialogue ended.");
        dialoguePanel.SetActive(false);
        inTalk = false;
        if (player != null)
        {
            player.cameraLock = false;
        }
    }
}