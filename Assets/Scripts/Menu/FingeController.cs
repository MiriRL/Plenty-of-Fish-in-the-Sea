using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FingeController : MonoBehaviour
{
    // There's a "main" screen showing the characters you can date, and each character has their own info screen you can go to\
    public GameObject mainScreen;
    public GameObject infoScreen;
    public GameObject characterInfo;
    public GameEvent OnSceneTransitionReady;

    // Info screen UI objects
    [SerializeField] private List<Image> infoScreenHearts;
    [SerializeField] private TextMeshProUGUI infoScreenNameText;
    [SerializeField] private Image infoScreenIcon;
    [SerializeField] private TextMeshProUGUI infoScreenDescription;

    // Info screen sprites
    [SerializeField] private Sprite filledHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite emptyIcon;
    
    // Private variables
    private List<GameObject> infoButtons = new List<GameObject>();
    private CoreManager coreManager;
    

    void Start()
    {
        coreManager = GetCoreManager();
        if (coreManager == null)
        {
            Debug.LogError("No core manager found.");
        }
        OpenMainScreen();
    }

    public void OpenMainScreen()
    {
        infoScreen.SetActive(false);
        

        foreach (Character character in coreManager.GetKnownCharacters())
        {
            GameObject newInfo = Instantiate(characterInfo, mainScreen.transform);
            Button infoButton = newInfo.GetComponentInChildren<Button>();
            if (infoButton == null)
            {
                Debug.LogError("Button component missing!");
            }
            else
            {
                infoButton.onClick.AddListener(() => OpenCharacterScreen(character));
            }
            TextMeshProUGUI buttonText = infoButton.GetComponentInChildren<TextMeshProUGUI>();
            Image iconImage = newInfo.GetComponentInChildren<Image>();
            List<Image> heartImages = newInfo.transform.GetChild(2).gameObject.GetComponentsInChildren<Image>().ToList();
            SetupCharInfo(character, buttonText, heartImages, iconImage);

            infoButtons.Add(newInfo);
        }

        mainScreen.SetActive(true);

    }

    public void OpenCharacterScreen(Character character)
    {
        mainScreen.SetActive(false);
        SetupCharInfo(character, infoScreenNameText, infoScreenHearts, infoScreenIcon);
        infoScreenDescription.text = character.GetCurrentFingeDescription();
        infoScreen.GetComponentInChildren<Button>().onClick.AddListener(() => SaveChosenCharacter(character));
        infoScreen.SetActive(true);
    }

    private void SaveChosenCharacter(Character character)
    {
        Debug.Log("Character saved");
        coreManager.currentCharacter = character;
        OnSceneTransitionReady.Raise();  // After the character is saved, we can unload the scene
    }

    private void SetupCharInfo(Character character, TextMeshProUGUI nameText, List<Image> heartSprites, Image icon)
    {
        nameText.SetText(character.characterName);

        for (int i = 0; i < heartSprites.Count; i++)
        {
            if (i < character.hearts)
            {
                heartSprites[i].sprite = filledHeart;
            }
            else 
            {
                heartSprites[i].sprite = emptyHeart;
            }
        }

        if (character.characterIcon != null) 
        {
            icon.sprite = character.characterIcon;
        } 
        else
        {
            icon.sprite = emptyIcon;
        }
    }

    public void StartDate()
    {
        // Reset Game states in core and start date
        coreManager.hasPlayedMinigame = false;
        coreManager.dialogueScore = 0;
        coreManager.minigameScore = 0;
        coreManager.LoadNewScene("DateScene");
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
