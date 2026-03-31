/*
Base code file taken from in-class example, written by Bret Jackson.
Edited for this project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueUIText;
    public TextMeshProUGUI characterNameText;
    public Canvas dialogueCanvas;
    public GameObject continueButton;
    public GameObject optionPanel;
    public TextMeshProUGUI[] optionsUI;
    public GameEvent onEmotionChange;

    private DialogueTree dialogue;
    private Sentence currentSentence = null;
    private string currEmotion = "";

    public void StartDialogue(DialogueTree dialogueTree){
        dialogue = dialogueTree;
        Character character = dialogueTree.character;
        currentSentence = dialogue.startingSentence;
        dialogueCanvas.enabled = true;
        characterNameText.text = dialogueTree.character.characterName;
        DisplaySentence();
    }

    public void AdvanceSentence(){
        currentSentence = currentSentence.nextSentence;
        DisplaySentence();
    }

    public void DisplaySentence(){
        if (currentSentence == null){
            EndDialogue();
            return;
        }
        HideOptions();
        string sentence = currentSentence.text;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void UpdateEmotion(string newEmotion)
    {
        Character character = dialogue.character;
        List<string> charEmotions = character.GetEmotionNames();
        if (charEmotions.Count == 0) {return;}

        Emotion updatedEmotion = character.GetEmotion(newEmotion);
        character.currentEmotion = updatedEmotion;
        onEmotionChange.Raise();
    }

    IEnumerator TypeSentence(string sentence){
        dialogueUIText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueUIText.text += letter;

            yield return new WaitForSeconds(0.05f);
        }

        if (currentSentence.HasOptions()){
            DisplayOptions();
        }
        else{
            continueButton.SetActive(true);
        }
    }

    void DisplayOptions(){
        if (currentSentence.options.Count <= optionsUI.Length){
            for (int i=0; i < currentSentence.options.Count; i++){
                optionsUI[i].text = currentSentence.options[i].text;
                optionsUI[i].transform.parent.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Too many options!");
        }
        optionPanel.SetActive(true);
    }

    void HideOptions(){
        continueButton.SetActive(false);
        foreach(TextMeshProUGUI option in optionsUI){
            option.transform.parent.gameObject.SetActive(false);
        }
        optionPanel.SetActive(false);
    }

    public void OptionOnClick(int index){
        Choice option = currentSentence.options[index];
        currentSentence = option.nextSentence;

        Debug.Log("Emotion: " + option.emotion);
        if (option.emotion != null && option.emotion != "" && option.emotion != currEmotion)
        {
            UpdateEmotion(option.emotion);
        }
        DisplaySentence();
    }

    void EndDialogue(){
        dialogueCanvas.enabled = false;
    }
}