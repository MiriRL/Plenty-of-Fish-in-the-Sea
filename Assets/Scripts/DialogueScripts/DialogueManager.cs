using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueUIText;
    public Canvas dialogueCanvas;
    public GameObject continueButton;
    public GameObject optionPanel;
    public TextMeshProUGUI[] optionsUI;

    private DialogueTree dialogue;
    private Sentence currentSentence = null;

    public void StartDialogue(DialogueTree dialogueTree){
        dialogue = dialogueTree;
        currentSentence = dialogue.startingSentence;
        dialogueCanvas.enabled = true;
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
        //dialogueUIText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
        if (option.onOptionSelected != null){
            option.onOptionSelected.Raise();
        }
        currentSentence = option.nextSentence;
        DisplaySentence();
    }

    void EndDialogue(){
        dialogueCanvas.enabled = false;
    }
}