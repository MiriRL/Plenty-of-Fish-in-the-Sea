using UnityEngine;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    public GameEvent updateScore;
    public GameEvent emotionChange;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private CharacterSpriteController characterSpriteController;
    private DialogueManager dialogueManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        if (updateScore != null)
        {
            updateScore.RegisterListener(null);
        }
    }

    public void EndDate()
    {
        Character character = dialogueManager.GetTalkingCharacter();
        // Update character's hearts based on score
        // Hearts can go up or down by a maximum of two
        int score = dialogueManager.GetCurrentScore();
        if (score > 2) { score = 2; }
        if (score < -2 ) { score = -2; }
        Debug.Log("Date score: " + score);
        int newHearts = character.hearts + score;
        if (newHearts > 3) { newHearts = 3; }
        if (newHearts < 0 ) { newHearts = 0; }
        character.hearts = newHearts;
        
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }

    private void onUpdateScore()
    {
        
    }

}
