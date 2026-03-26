using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

// Makes a dialogue tree from a JSON file in the Dialogues folder.
public class DialogueFromJSON : ScriptableObject
{
    public DialogueTree dialogueTree;
    
    private List<Sentence> sentences;
    private List<string> createdIds;
    private string fileName;
    private SentenceWrapper wrapper;
    
    // Takes the character name and the local path of the JSON file relative to the Dialogues folder in Resources.
    public void LoadDialogueTree(string characterName, string JSONFileName)
    {
        fileName = JSONFileName;
        
        dialogueTree = ScriptableObject.CreateInstance<DialogueTree>();
        dialogueTree.character.characterName = characterName;

        sentences = new List<Sentence>();
        createdIds = new List<string>();

        MakeSentenceTree();
    }

    // Given a dialogue tree, writes a JSON with the given file name. File is located in Resources/Dialogues
    public void MakeDialogueJSON(DialogueTree dialogueTree, string fileName)
    {
        sentences = new List<Sentence>();
        createdIds = new List<string>();
        
        // Make a file for the data. Will need to be moved locations after.
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Make the dialogue tree into a list of JSON sentences & options. The first sentence is the root.
        string jsonText = CreateJsonString(dialogueTree);
        
        // Write the JSON sentences to the file
        File.WriteAllText(filePath, jsonText);

        // Hope it works lol
        Debug.Log("File written to " + filePath);
        return;
    }

    private void MakeSentenceTree()
    {
        // Reading and unpacking the JSON info
        TextAsset json = Resources.Load<TextAsset>(Path.Combine("Dialogues", fileName));
        if (json == null)
        {
            Debug.LogError("JSON file not found.");
            return;
        }

        string jsonText = json.text;
        wrapper = JsonUtility.FromJson<SentenceWrapper>(jsonText);
        PrintWrapperAsIds();

        // Start with the first sentence.
        // Debug.Log("Root id: " + wrapper.sentences[0].id);
        MakeSentence(wrapper.sentences[0].id);
    }

    // A recursive function which follows the dialogue tree down and builds the tree
    private Sentence MakeSentence(string sentenceID)
    {
        // Debug.Log("Current id: " + sentenceID);
        // Check if the sentence has already been made
        if (createdIds.Contains(sentenceID))
        {
            return GetSentenceFromId(sentenceID);
        }

        JSONSentence jsonSentence = GetJsonSentenceFromId(sentenceID);
        
        // Otherwise make the new sentence and add it to the list.
        Sentence newSentence = ScriptableObject.CreateInstance<Sentence>();
        newSentence.text = jsonSentence.text;
        newSentence.id = jsonSentence.id;

        // If it's the first sentence in the list, make it the first sentence of the dialogue tree.
        if (sentences.Count == 0)
        {
            dialogueTree.startingSentence = newSentence;
        }
        
        sentences.Add(newSentence);
        createdIds.Add(newSentence.id);

        // If it's a leaf node, return and don't recurse
        if (jsonSentence.options.Count == 0 && jsonSentence.idNextSentence == "")
        {
            return GetSentenceFromId(sentenceID);
        }

        // Otherwise add in the options or next sentence and recurse over them
        if (jsonSentence.options.Count == 0)
        {
            if (!createdIds.Contains(jsonSentence.idNextSentence)) 
            {
                newSentence.nextSentence = MakeSentence(jsonSentence.idNextSentence);
            }
        } 
        else
        {
            foreach (JSONChoice option in jsonSentence.options)
            {
                Choice newChoice = new Choice();
                newChoice.id = option.id;
                newChoice.text = option.text;
                newChoice.score = option.score;
                if (!createdIds.Contains(option.idNextSentence)) 
                {
                    newChoice.nextSentence = MakeSentence(option.idNextSentence);
                }
                newSentence.options.Add(newChoice);
            }
        }
        
        return newSentence;
    }

    private string CreateJsonString(DialogueTree dialogueTree)
    {
        wrapper = new SentenceWrapper();
        Sentence firstSentence = dialogueTree.startingSentence;

        // Go ahead and add the first sentence to the list first so it's saved as the root
        wrapper.sentences.Add(MakeJSONSentenceFromSentence(firstSentence));
        createdIds.Add(firstSentence.id);
        recurseJSONTree(firstSentence);

        return JsonUtility.ToJson(wrapper, true);
    }

    private void recurseJSONTree(Sentence sentence) 
    {
        PrintWrapperAsIds();
        if (sentence == null) { return; }
        
        if (!sentence.HasOptions() && sentence.nextSentence == null)
        {
            AddSentenceToWrapper(MakeJSONSentenceFromSentence(sentence));
            return;
        }

        if (sentence.HasOptions())
        {
            // Debug.Log(sentence.id + " has options");
            JSONSentence writableSentence = MakeJSONSentenceFromSentence(sentence);
            foreach (Choice option in sentence.options)
            {
                writableSentence.options.Add(MakeJSONChoiceFromChoice(option));
                // Debug.Log("Option added: " + option.id);

                if (!createdIds.Contains(option.nextSentence.id))
                {
                    recurseJSONTree(option.nextSentence);
                }
            }

            AddSentenceToWrapper(writableSentence);
            return;
        }

        AddSentenceToWrapper(MakeJSONSentenceFromSentence(sentence));
        if (!createdIds.Contains(sentence.nextSentence.id))
        {
            recurseJSONTree(sentence.nextSentence);
        }
        return;
    }

    private void AddSentenceToWrapper(JSONSentence sentence)
    {
        // If it's already in the wrapper, don't add it again
        if (createdIds.Contains(sentence.id))
        {
            return;
        }

        wrapper.sentences.Add(sentence);
        createdIds.Add(sentence.id);
        return;
    }

    private JSONSentence MakeJSONSentenceFromSentence(Sentence sentence)
    {
        JSONSentence newSentence = new JSONSentence();
        newSentence.id = sentence.id;
        newSentence.text = sentence.text;
        if (sentence.nextSentence != null) 
        { 
            newSentence.idNextSentence = sentence.nextSentence.id; 
        }
        return newSentence;
    }

    private JSONChoice MakeJSONChoiceFromChoice(Choice option)
    {
        JSONChoice newOption = new JSONChoice();
        newOption.id = option.id;
        newOption.text = option.text;
        newOption.score = option.score;
        if (option.nextSentence != null) 
        { 
            newOption.idNextSentence = option.nextSentence.id; 
        }
        return newOption;
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
    private JSONSentence GetJsonSentenceFromId(string id)
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

    // For debugging
    private void PrintWrapperAsIds()
    {
        Debug.Log("wrapper ids: " + string.Join(" ", wrapper.sentences.Select(i => i.id)));
        return;
    }
}

[System.Serializable]
// Class which holds sentence data in a way readable by a JSON file
public class JSONSentence
{
    public string id;
    public string text;
    public string idNextSentence;
    public List<JSONChoice> options = new List<JSONChoice>();
    
}

[System.Serializable]
// Class which holds option data in a way readable by a JSON file
public class JSONChoice
{
    public string id;
    public string text;
    public string idNextSentence;
    public int score;
    
}

[System.Serializable]
public class SentenceWrapper
{
    public List<JSONSentence> sentences = new List<JSONSentence>();
}
