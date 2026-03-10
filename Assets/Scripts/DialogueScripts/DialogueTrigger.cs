using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTree dialogue;
    public DialogueManager dialogueManager;

    public void OnTriggerEnter(Collider other){
        dialogueManager.StartDialogue(dialogue);
    }
}