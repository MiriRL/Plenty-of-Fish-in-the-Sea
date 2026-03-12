using Unity.VisualScripting;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private DialogueTree dialogueTree;
    [SerializeField] private bool writeTreeToJSON;
    
    void Start()
    {
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogueTree);
        
        if (writeTreeToJSON) {
            DialogueFromJSON dialogueFromJSON = ScriptableObject.CreateInstance<DialogueFromJSON>();
            dialogueFromJSON.MakeDialogueJSON(dialogueTree, "testTree.json");
        }
    }
}
