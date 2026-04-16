using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    private DialogueManager dialogueManager;
    private int currScore;
    private string minigameSceneName;
    private Character character;
    [SerializeField] private List<Character> knownCharacters;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadCharacter();
        if (character == null)
        {
            return;
        }
        dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        characterSpriteController.ChangeCharacter(character);
        Debug.Log(character);
        
        if (character.chooseDialogue() == null)
        {
            Debug.Log("No Dialogue!");
        }
        dialogueManager.StartDialogue(character.chooseDialogue());
        currScore = 0;
    }

    private void LoadCharacter()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "ChosenCharacter");
        if (!File.Exists(filePath))
        {
            return;
        }
        string json = File.ReadAllText(filePath);

        Data data = JsonUtility.FromJson<Data>(json);
        if (data == null)
        {
            return;
        }
    
        Debug.Log("Loaded " + data.charName);

        foreach (Character chara in knownCharacters)
        {
            if (chara.characterName == data.charName)
            {
                character = chara;
            }
        }
    }

    public void SetCharacter(Character newCharacter)
    {
        character = newCharacter;
    }

    public void EndDate()
    {
        // Update character's hearts based on score
        // Hearts can go up or down by a maximum of two
        UpdateScore(dialogueManager.GetCurrentScore());
        
        int newHearts = character.hearts + currScore;
        if (newHearts > 3) { newHearts = 3; }
        if (newHearts < 0 ) { newHearts = 0; }
        character.hearts = newHearts;
        
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }

    public void UpdateCharSprite()
    {
        characterSpriteController.UpdateSprite();
    }

    public void StartMinigame(Character character)
    {
        // Load the minigame scene additive
        switch (character.name)
        {
            case "Goldfish":
                minigameSceneName = "MatchingGame";
                break;
            case "Salmon":
                minigameSceneName = "FishRace";
                break;
            default:
                minigameSceneName = "";
                break;
        }
        
        if (minigameSceneName != "")
        {
            SceneManager.LoadSceneAsync(minigameSceneName, LoadSceneMode.Single);
        }
    }

    public void OnEndMinigame()
    {
        // Update currScore based on minigame score
        return;
    }

    private void UpdateScore(int score)
    {
        if (score > 2) { score = 2; }
        if (score < -2 ) { score = -2; }
        Debug.Log("Date score: " + score);
        currScore += score;
    }

    public class Data
    {
        public string charName;
    }

}
