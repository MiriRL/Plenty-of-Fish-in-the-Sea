using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public GameObject phone;
    public GameObject homeScreen;
    public GameObject fingeScreen;
    public GameEvent OnSceneReady;
    private CoreManager core;

    void Start()
    {
        core = GetCoreManager();
        if(core == null)
        {
            Debug.LogError("No Core found");
        }
    }
    public void ToggleMenu()
    {
        phone.SetActive(!phone.activeSelf);
        
        if (phone.activeSelf)
        {
            // Always open the home screen
            homeScreen.SetActive(true);
            fingeScreen.SetActive(false);
        }
    }

    public void GoHome()
    {
        homeScreen.SetActive(true);
        fingeScreen.SetActive(false);
    }

    public void OpenFinge()
    {
        fingeScreen.SetActive(true);
        homeScreen.SetActive(false);
    }

    public void OpenDebugScene()
    {
        OnSceneReady.Raise();
        core.LoadNewScene("DebugSceneMenu");
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
