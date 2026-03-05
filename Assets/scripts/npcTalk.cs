using UnityEngine;

public class npcTalk : MonoBehaviour
{
    private bool inRange;

    public GameObject uiElement;
    public DialogueManager dialogueManager;

    public DialogueLineSO startDialogue;

    private playerController playerController;

    void Start()
    {
        uiElement.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange && !dialogueManager.inTalk)
        {
            dialogueManager.StartDialogue(startDialogue);

            if (playerController != null)
                playerController.cameraLock = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            if (playerController != null)
                playerController.cameraLock = false;

            dialogueManager.inrange = false;
            uiElement.SetActive(false);
            inRange = false;

            playerController = null;
        }
    }
}