using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FingeController : MonoBehaviour
{
    // There's a "main" screen showing the characters you can date, and each character has their own info screen you can go to\
    public GameObject mainScreen;
    public GameObject infoScreen;
    public GameObject characterInfo;
    public List<Character> knownCharacters;
    // Info screen objects
    [SerializeField] private List<Image> infoScreenHearts;
    [SerializeField] private TextMeshProUGUI infoScreenNameText;
    [SerializeField] private Image infoScreenIcon;
    [SerializeField] private TextMeshProUGUI infoScreenDescription;

    [SerializeField] private Sprite filledHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite emptyIcon;
    private List<GameObject> infoButtons = new List<GameObject>();
    

    void Start()
    {
        // Eventually have known characters dynamically update
        openMainScreen();
    }

    public void openMainScreen()
    {
        infoScreen.SetActive(false);
        

        foreach (Character character in knownCharacters)
        {
            GameObject newInfo = Instantiate(characterInfo, mainScreen.transform);
            Button infoButton = newInfo.GetComponentInChildren<Button>();
            if (infoButton == null)
            {
                Debug.LogError("Button component missing!");
            }
            else
            {
                infoButton.onClick.AddListener(() => openCharacterScreen(character));
            }
            TextMeshProUGUI buttonText = infoButton.GetComponentInChildren<TextMeshProUGUI>();
            Image iconImage = newInfo.GetComponentInChildren<Image>();
            List<Image> heartImages = newInfo.transform.GetChild(2).gameObject.GetComponentsInChildren<Image>().ToList();
            setupCharInfo(character, buttonText, heartImages, iconImage);

            infoButtons.Add(newInfo);
        }

        mainScreen.SetActive(true);

    }

    public void openCharacterScreen(Character character)
    {
        mainScreen.SetActive(false);
        setupCharInfo(character, infoScreenNameText, infoScreenHearts, infoScreenIcon);
        infoScreenDescription.text = character.GetCurrentFingeDescription();
        infoScreen.SetActive(true);
    }

    private void setupCharInfo(Character character, TextMeshProUGUI nameText, List<Image> heartSprites, Image icon)
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

        if (character.characterSprite != null) 
        {
            icon.sprite = character.characterSprite;
        } 
        else
        {
            icon.sprite = emptyIcon;
        }
    }
}
