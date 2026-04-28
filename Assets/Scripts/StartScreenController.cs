using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    public GameEvent OnSceneReady;
    private CoreManager core;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnStartClick()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("CoreScene", LoadSceneMode.Single);
    }

    public void OnMatchTestClick()
    {
        Debug.Log("Starting matching");
        core = GetCoreManager();
        if(core == null)
        {
            Debug.LogError("No Core found");
        }
        core.LoadNewScene("MatchingGame");
        OnSceneReady.Raise();
    }

    public void OnQTETestClick()
    {
        Debug.Log("Starting QTE");
        core = GetCoreManager();
        if(core == null)
        {
            Debug.LogError("No Core found");
        }
        core.LoadNewScene("MashingScene");
        OnSceneReady.Raise();
    }

    public void OnFishRaceTestClick()
    {
        Debug.Log("Starting matching");
        core = GetCoreManager();
        if(core == null)
        {
            Debug.LogError("No Core found");
        }
        core.LoadNewScene("FishRace");
        OnSceneReady.Raise();
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
