using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    public GameEvent OnSceneTransitionReady;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    private DialogueManager dialogueManager;
    private int cumulativeScore;
    private string minigameSceneName;
    private Character character;
    private List<Character> knownCharacters;
    private CoreManager coreManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coreManager = GetCoreManager();
        if (coreManager == null)
        {
            Debug.LogError("No core manager found.");
        }
        
        character = coreManager.currentCharacter;
        if (character == null)
        {
            Debug.LogError("No character for loaded date!");
            OnSceneTransitionReady.Raise();
            coreManager.LoadNewScene("HomeScreen");
            return;
        }

        dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        characterSpriteController.ChangeCharacter(character);
        cumulativeScore = 0;
        
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (character.chooseDialogue() == null)
        {
            Debug.LogError("No Dialogue!");
        }
        
        dialogueManager.StartDialogue(character.chooseDialogue());
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
        
        int newHearts = character.hearts + cumulativeScore;
        if (newHearts > 3) { newHearts = 3; }
        if (newHearts < 0 ) { newHearts = 0; }
        character.hearts = newHearts;
        
        OnSceneTransitionReady.Raise();
        coreManager.LoadNewScene("HomeScreen");
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
            OnSceneTransitionReady.Raise();
            coreManager.LoadNewScene(minigameSceneName);
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
        cumulativeScore += score;
    }

    private CoreManager GetCoreManager()
    {
        Scene coreScene = SceneManager.GetSceneByName("CoreScene");
        GameObject[] coreObjects = coreScene.GetRootGameObjects();

        foreach (GameObject gameObject in coreObjects) 
        {
            if (gameObject.CompareTag("GameController"))
            {
                return gameObject.GetComponent<CoreManager>();
            }
        }

        Debug.LogError("No Core Manager found!");
        return null;
    }

}
