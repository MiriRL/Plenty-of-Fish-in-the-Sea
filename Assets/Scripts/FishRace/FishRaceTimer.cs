using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishRaceTimer : MonoBehaviour
{
    public float timeRemaining;
    public bool timeRunning = false;
    public TextMeshProUGUI timeText;

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
}
