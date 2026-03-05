using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public Sprite characterSprite;
    [TextArea(2, 5)]
    public string text;

    [Header("Branching")]
    [SerializeReference]
    public DialogueLine nextLine;

    [SerializeReference]
    public DialogueChoice[] choices;

    [Header("Optional Event")]
    public string eventName;
}