using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{
    // Dialogue that will play on screen start whenever it's loaded
    public DialogueTree startDialogue;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private SpriteRenderer momSprite;
    private CoreManager coreManager;
    
    void Start()
    {
        coreManager = GetCoreManager();
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(startDialogue);
        ShowMomSprite();
    }

    public void ShowMomSprite()
    {
        momSprite.enabled = true;
    }

    public void HideMomSprite()
    {
        momSprite.enabled = false;
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
