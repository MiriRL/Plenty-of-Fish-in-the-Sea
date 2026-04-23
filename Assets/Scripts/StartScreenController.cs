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
        SceneManager.LoadScene("CoreScene", LoadSceneMode.Single);
    }

    public void OnMatchTestClick()
    {
        Debug.Log("Starting matching");
        SceneManager.LoadScene("MatchingGame", LoadSceneMode.Additive);
    }

    public void OnQTETestClick()
    {
        Debug.Log("Starting QTE");
        SceneManager.LoadScene("MashingScene", LoadSceneMode.Additive);
    }

    public void OnFishRaceTestClick()
    {
        Debug.Log("Starting matching");
        SceneManager.LoadScene("FishRace", LoadSceneMode.Additive);
    }
}
