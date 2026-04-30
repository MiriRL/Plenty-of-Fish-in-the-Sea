using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKill : MonoBehaviour
{
    Vector2 startPos;

    public GameEvent onSceneTransitionReady;

    private CoreManager coreManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SceneManager.LoadScene("CoreScene", LoadSceneMode.Additive);

        coreManager = GetCoreManager();
        if(coreManager == null)
        {
            Debug.Log("No coremanager found");
        }
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Obstacle"))
        {
            coreManager.minigameScore = 0;
            coreManager.LoadNewScene("DateScene");
            onSceneTransitionReady.Raise();
        //     transform.position = startPos;
        //     //Replace with ending the minigame, either ending screen or sending back to the date
        //     SceneManager.LoadSceneAsync("DateScene", LoadSceneMode.Single);
        }
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
