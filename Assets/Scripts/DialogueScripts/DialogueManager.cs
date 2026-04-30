/*
Base code file taken from in-class example, written by Bret Jackson.
Edited for this project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueUIText;
    public TextMeshProUGUI characterNameText;
    public Canvas dialogueCanvas;
    public GameObject continueButton;
    public GameObject optionPanel;
    public TextMeshProUGUI[] optionsUI;
    public GameEvent OnStartDialogue;
    public GameEvent OnEndDialogue;
    public GameEvent OnStartMinigame;
    public GameEvent OnUpdateCharSprite;

    private DialogueTree dialogue;
    private Sentence currentSentence = null;
    private string currEmotion = "";
    private int currScore;

    public void StartDialogue(DialogueTree dialogueTree){
        dialogue = dialogueTree;
        currentSentence = dialogue.startingSentence;
        dialogueCanvas.enabled = true;
        characterNameText.text = dialogueTree.character.characterName;
        currScore = 0;
        DisplaySentence();
        OnStartDialogue.Raise();
    }

    public void AdvanceSentence(){
        currentSentence = currentSentence.nextSentence;
        DisplaySentence();
    }

    public int GetCurrentScore()
    {
        return currScore;
    }

    public void DisplaySentence(){
        if (currentSentence == null){
            EndDialogue();
            return;
        }
        HideOptions();
        string sentence = currentSentence.text;

        if (sentence == "Plays the game" || sentence == "Plays the racing game")
        {
            OnStartMinigame.Raise();
            return;
        }

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
        OnUpdateCharSprite.Raise();
    }

    IEnumerator TypeSentence(string sentence){
        dialogueUIText.text = "";
        float baseSpeed = 0.025f;
        foreach(char letter in sentence.ToCharArray()){
            if (Keyboard.current.spaceKey.isPressed)
            {
                dialogueUIText.text = sentence;
                break;
            }

            dialogueUIText.text += letter;

            // Wait longer if the dialogue has a ... or ends in punctuation for natural flow
            if (dialogueUIText.text.EndsWith("..."))
            {
                yield return new WaitForSeconds(15.0f*baseSpeed);
            } else if (dialogueUIText.text.EndsWith(".") || dialogueUIText.text.EndsWith("?") || dialogueUIText.text.EndsWith("!"))
            {
                yield return new WaitForSeconds(5.0f*baseSpeed);
            }

            yield return new WaitForSeconds(baseSpeed);
            //
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
        if (option.onOptionSelected != null)
        {
            option.onOptionSelected.Raise();
        }

        Debug.Log("Emotion: " + option.emotion);
        if (option.emotion != null && option.emotion != "" && option.emotion != currEmotion)
        {
            UpdateEmotion(option.emotion);
        }
        if (option.score != 0)
        {
            currScore += option.score;
        }
        DisplaySentence();
    }

    void EndDialogue(){
        dialogueCanvas.enabled = false;
        OnEndDialogue.Raise();
    }
}