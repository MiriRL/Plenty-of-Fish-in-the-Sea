using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

// Makes a dialogue tree from a JSON file in the Dialogues folder.
public class DialogueFromJSON : MonoBehaviour
{
    public DialogueTree dialogueTree;
    
    private Sentence[] sentences;
    private string[] createdIds;
    private string fileName;
    private SentenceWrapper wrapper;
    
    // Takes the character name and the local path of the JSON file relative to the Dialogues folder in Resources.
    public void OnLoadDialogueTree(string characterName, string JSONFileName)
    {
        fileName = JSONFileName;
        
        dialogueTree = ScriptableObject.CreateInstance<DialogueTree>();
        dialogueTree.characterName = characterName;

        MakeSentenceTree();
    }

    private void MakeSentenceTree()
    {
        TextAsset json = Resources.Load<TextAsset>(Path.Combine("Dialogues", fileName));
        if (json == null)
        {
            Debug.LogError("JSON file not found.");
            return;
        }

        string jsonText = json.text;
        wrapper = JsonUtility.FromJson<SentenceWrapper>(jsonText);

        foreach (JSONSentence jsonSentence in wrapper.sentences)
        {
            MakeSentence(jsonSentence.id);
        }
    }

    private Sentence MakeSentence(string sentenceID)
    {
        // Check if the sentence has already been made
        if (createdIds.Contains(sentenceID))
        {
            return GetSentenceFromId(sentenceID);
        }

        JSONSentence jsonSentence = GetJsonSentenceFromId(wrapper, sentenceID);
        
        // Otherwise make the new sentence and add it to the list.
        Sentence newSentence = ScriptableObject.CreateInstance<Sentence>();
        newSentence.text = jsonSentence.text;
        newSentence.id = jsonSentence.id;

        // If it's the first sentence in the list, make it the first sentence of the dialogue tree.
        if (sentences.Length == 0)
        {
            dialogueTree.startingSentence = newSentence;
        }
        
        sentences.Append(newSentence);
        createdIds.Append(newSentence.id);

        // Add in the options or next sentence
        if (jsonSentence.options.Length == 0)
        {
            newSentence.nextSentence = MakeSentence(jsonSentence.idNextSentence);
        } 
        else
        {
            foreach (JSONChoice option in jsonSentence.options)
            {
                Choice newChoice = new Choice();
                newChoice.id = option.id;
                newChoice.text = option.text;
                newChoice.nextSentence = MakeSentence(option.idNextSentence);
            }
        }
        
        return newSentence;
    }

    private Sentence GetSentenceFromId(string id)
    {
        foreach (Sentence sentence in sentences)
        {
            if (sentence.id == id)
            {
                return sentence;
            }
        }
        Debug.Log("Sentence ID not found in list.");
        return null;
    }
    private JSONSentence GetJsonSentenceFromId(SentenceWrapper wrapper, string id)
    {
        foreach (JSONSentence sentence in wrapper.sentences)
        {
            if (sentence.id == id)
            {
                return sentence;
            }
        }
        Debug.Log("Sentence ID not found in JSON list.");
        return null;
    }
}

[System.Serializable]
// Class which holds sentence data in a way readable by a JSON file
public class JSONSentence
{
    public string id;
    public string text;
    public string idNextSentence;
    public JSONChoice[] options;
    
}

[System.Serializable]
// Class which holds option data in a way readable by a JSON file
public class JSONChoice
{
    public string id;
    public string text;
    public string idNextSentence;
    
}

[System.Serializable]
public class SentenceWrapper
{
    public JSONSentence[] sentences;
}
