using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartClick()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }

    public void OnMatchTestClick()
    {
        Debug.Log("Starting matching");
        SceneManager.LoadScene("MatchingGame", LoadSceneMode.Single);
    }

    public void OnQTETestClick()
    {
        Debug.Log("Starting QTE");
        SceneManager.LoadScene("MashingScene", LoadSceneMode.Single);
    }

    public void OnFishRaceTestClick()
    {
        Debug.Log("Starting matching");
        SceneManager.LoadScene("FishRace", LoadSceneMode.Single);
    }
}
