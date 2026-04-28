using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishRaceTimer : MonoBehaviour
{
    public float timeRemaining;
    public bool timeRunning = false;
    public TextMeshProUGUI timeText;

    private CoreManager core;
    public GameEvent onSceneTransitionReady;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRunning = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(timeRemaining);
        if (timeRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Game won!");
                core.LoadNewScene("DateScene");
                core.minigameScore = 1;
                onSceneTransitionReady.Raise();
                // SceneManager.UnloadSceneAsync("FishRace");
                //Replace with ending the game, returning to date with win
                timeRemaining = 0;
                timeRunning = false;
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay/60);
        float seconds = Mathf.FloorToInt(timeToDisplay%60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
