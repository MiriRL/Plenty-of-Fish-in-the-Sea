using Unity.VisualScripting;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private DialogueTree dialogueTree;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    [Tooltip("The name of the JSON file which must be located in Resources/Dialogues")]
    [SerializeField] private string DialogueJSONFileName;
    [SerializeField] private bool writeTreeToJSON;
    
    void Start()
    {
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        // dialogueManager.StartDialogue(dialogueTree);
        DialogueFromJSON dialogueFromJSON = ScriptableObject.CreateInstance<DialogueFromJSON>();
        dialogueFromJSON.LoadDialogueTree(characterSpriteController.talkingCharacter, DialogueJSONFileName);
        dialogueManager.StartDialogue(dialogueFromJSON.dialogueTree);
        
        if (writeTreeToJSON) {
            // DialogueFromJSON dialogueFromJSON = ScriptableObject.CreateInstance<DialogueFromJSON>();
            dialogueFromJSON.MakeDialogueJSON(dialogueTree, "testTree.json");
        }
    }
}
