using Unity.VisualScripting;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    // Dialogue that will play on screen start whenever it's loaded
    public DialogueTree startDialogue;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    
    void Start()
    {
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(startDialogue);
    }
}
