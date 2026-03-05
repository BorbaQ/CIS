using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    [SerializeReference]
    public DialogueLine nextLine;
}