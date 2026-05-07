using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    public GameEvent OnSceneTransitionReady;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    [SerializeField] private Character mom;
    [SerializeField] private Character salmon;
    private DialogueManager dialogueManager;
    private float cumulativeScore;
    private string minigameSceneName;
    private Character character;
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
        cumulativeScore = 0.0f;
        
        StartDialogue();
    }

    private void StartDialogue()
    {
        DialogueTree chosenDialogue = null;
        if (coreManager.hasPlayedMinigame)
        {
            chosenDialogue = character.ChooseMinigameEndDialogue();
        }
        else
        {
            chosenDialogue = character.ChooseDialogue();
        }
        
        if (chosenDialogue == null)
        {
            Debug.LogError("No Dialogue!");
        }
        
        dialogueManager.StartDialogue(chosenDialogue);
    }

    public void SetCharacter(Character newCharacter)
    {
        character = newCharacter;
    }

    public void EndDate()
    {
        if (!coreManager.hasPlayedMinigame)
        {
            // Only do everything else after the minigame has played
            // Otherwise it's an early failure and we return to the home screen
            FailDate();
            return;
        }
        // Update character's hearts based on score
        // Hearts can go up or down by a maximum of two
        UpdateScore(coreManager.minigameScore + coreManager.dialogueScore);
        
        int newHearts = (int) (character.hearts + cumulativeScore);
        if (newHearts > 3) { newHearts = 3; }
        if (newHearts < 0 ) { newHearts = 0; }
        character.hearts = newHearts;

        // Mom also gets another heart
        MomScoreUpdate();
        
        
        OnSceneTransitionReady.Raise();
        coreManager.LoadNewScene("HomeScreen");
    }

    public void UpdateCharSprite()
    {
        characterSpriteController.UpdateSprite();
    }

    public void StartMinigame()
    {
        // Save the dialogue score in the core
        coreManager.dialogueScore = ((float) dialogueManager.GetCurrentScore()) / 2.0f;
        Debug.Log("Dialogue final score: " + coreManager.dialogueScore);
        coreManager.hasPlayedMinigame = true;
        // Load the minigame scene additive
        switch (coreManager.currentCharacter.name)
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

    private void FailDate()
    {
        MomScoreUpdate();  // Mom still updates bc you finished a date
        coreManager.UpdateKnownCharacters(salmon);  // Still update known chars even if you didn't play the minigame
        OnSceneTransitionReady.Raise();
        coreManager.LoadNewScene("HomeScreen");
        // Don't update character score bc score is zero?
    }

    private void MomScoreUpdate()
    {
        if (mom.hearts < 3)
        {    
            mom.hearts += 1;
        }
    }

    private void UpdateScore(float score)
    {
        Debug.Log("Base score: " + score);
        // Keeps the score from going up or down more than 2 points at a time
        if (score > 2) { score = 2; }
        if (score < -2 ) { score = -2; }
        cumulativeScore += score;
        Debug.Log("Date total score: " + cumulativeScore);
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
