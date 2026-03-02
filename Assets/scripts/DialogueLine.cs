using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;   // Who is speaking
    public Sprite characterSprite; // Character portrait
    [TextArea(2, 5)]
    public string text;            // Dialogue text
}