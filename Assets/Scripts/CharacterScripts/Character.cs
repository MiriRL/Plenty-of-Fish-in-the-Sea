using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character Object/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    [Tooltip("The number of points the player currently has with the character.")]
    public int hearts;
    [SerializeField] private List<Emotion> emotions = new List<Emotion>();

    [SerializeField] private List<DialogueTree> dialogues = new List<DialogueTree>();
    [SerializeField] private List<FingeInfo> fingeDescriptions = new List<FingeInfo>();
    [SerializeField] private List<Sprite> gifts = new List<Sprite>();

    // private List<DialogueTree> playedDialogues  -> do something like this to handle repeats if we add more

    // For now, I assume there is exactly one dialogue tree for each number of hearts
    public DialogueTree chooseDialogue()
    {
        if (dialogues.Count == 0)
        {
            Debug.LogError("No dialogues found for " + characterName);
            return null;
        }

        foreach (DialogueTree dialogue in dialogues)
        {
            if (dialogue.requiredHearts == hearts) {
                return dialogue;
            }
        }

        Debug.LogError("No dialogue found for " + characterName + " with " + hearts.ToString() + " number of hearts.");
        return null;
    }

    public string getCurrentFingeDescription()
    {
        if (fingeDescriptions.Count == 0)
        {
            Debug.Log("No finge descriptions found for " + characterName);
            return "";
        }

        foreach (FingeInfo info in fingeDescriptions)
        {
            if (info.associatedHearts == hearts) {
                return info.availableInfo;
            }
        }

        Debug.Log("No finge descriptions found for " + characterName + " with " + hearts.ToString() + " number of hearts.");
        return "";
    }

    public List<Sprite> getCurrentGifts()
    {
        List<Sprite> currentGifts = new List<Sprite>();
        if (gifts.Count == 0 || hearts == 0)
        {
            Debug.Log("No gifts from " + characterName);
            return currentGifts;  // Return an empty list for no gifts
        }

        for (int i = 0; i < hearts; i++)
        {
            currentGifts.Add(gifts[i]);
        }

        return currentGifts;
    }
}

[System.Serializable]
public class Emotion
{
    public string emotionName;
    public Sprite emotionSprite;
}

[System.Serializable]
public class FingeInfo
{
    public int associatedHearts;
    [TextArea(3, 10)]
    public string availableInfo;
}
