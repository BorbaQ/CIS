using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Line")]
public class DialogueLineSO : ScriptableObject
{
    [Header("Speaker Info")]
    public string characterName;
    public Sprite characterSprite;

    [TextArea(2, 5)]
    public string text;

    [Header("Branching")]
    public DialogueLineSO nextLine;         // For sequential dialogue
    public DialogueChoiceSO[] choices;      // For player options

    [Header("Optional Event")]
    public string eventName;                // e.g., "KnockoutPlayer"
}

[System.Serializable]
public class DialogueChoiceSO
{
    public string choiceText;
    public DialogueLineSO nextLine;         // Where this choice leads
}