using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private DialogueTree dialogueTree;
    
    void Start()
    {
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogueTree);
    }
}
