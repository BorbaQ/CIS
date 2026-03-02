using UnityEngine;

public class npcTalk : MonoBehaviour
{
    private bool inRange;
    public GameObject uiElement;
    public DialogueManager dialogueManager;

    private playerController playerController; // Reference to player script

    void Start()
    {
        uiElement.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            Debug.Log("E was pressed!");
            dialogueManager.dialoguePanel.SetActive(true);
            dialogueManager.NextLine(); // Start dialogue

            // Lock the player's camera when dialogue starts
            if (playerController != null)
                playerController.cameraLock = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController script from the player
            playerController = other.GetComponent<playerController>();

            dialogueManager.inrange = true;
            uiElement.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Unlock the camera when leaving
            if (playerController != null)
                playerController.cameraLock = false;

            dialogueManager.inrange = false;
            uiElement.SetActive(false);
            inRange = false;

            playerController = null; // Clean up reference
        }
    }
}