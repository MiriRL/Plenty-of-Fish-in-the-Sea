using Unity.VisualScripting;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    // Dialogue that will play on screen start whenever it's loaded
    public DialogueTree startDialogue;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    // [Tooltip("The name of the JSON file which must be located in Resources/Dialogues")]
    // [SerializeField] private string DialogueJSONFileName;
    // [SerializeField] private bool writeTreeToJSON;
    
    void Start()
    {
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(startDialogue);
        // Code to start the dialogue from a JSON instead. Switch out for the line above if prefered.
        // DialogueFromJSON dialogueFromJSON = ScriptableObject.CreateInstance<DialogueFromJSON>();
        // dialogueFromJSON.LoadDialogueTree(characterSpriteController.talkingCharacter, DialogueJSONFileName);
        // dialogueManager.StartDialogue(dialogueFromJSON.dialogueTree);
        
        // 
        // if (writeTreeToJSON) {
        //     // DialogueFromJSON dialogueFromJSON = ScriptableObject.CreateInstance<DialogueFromJSON>();
        //     dialogueFromJSON.MakeDialogueJSON(dialogueTree, "testTree.json");
        // }
    }
}
