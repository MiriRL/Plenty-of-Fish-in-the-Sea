using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{
    // Dialogue that will play on screen start whenever it's loaded
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private SpriteRenderer momSprite;
    [SerializeField] private Character mom;
    private CoreManager coreManager;
    
    void Start()
    {
        coreManager = GetCoreManager();
        DialogueManager dialogueManager = dialogueCanvas.GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(ChooseMomDialogue());
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
    
    private DialogueTree ChooseMomDialogue()
    {
        return mom.ChooseDialogue();
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
