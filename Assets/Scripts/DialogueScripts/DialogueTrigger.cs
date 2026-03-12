/*
Base code file taken from in-class example, written by Bret Jackson.
*/

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