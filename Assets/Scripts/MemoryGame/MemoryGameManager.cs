using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

//manages the Memory Game

//using tutorial: https://www.youtube.com/watch?v=HajiNmv4UNY
public class MemoryGameManager : MonoBehaviour
{
    //Serializable Fields
    [SerializeField] MGCardBehavior cardPrefab;

    //grid that the cards will appear on
    [SerializeField] Transform gridTransform;

    //sprites to be displayed on the cards -- used for identifying pairs
    [SerializeField] Sprite[] sprites;
    // text to be displayed on the cards
    [SerializeField] List<string> textList;

    // the panel displayed on game win
    [SerializeField] GameObject overlayPanel;
    // text displayed on overlayPanel
    [SerializeField] TextMeshProUGUI scoreText;
    


    private List<Sprite> spritePairs;
    private MGCardBehavior firstSelected;
    private MGCardBehavior secondSelected;
    private int matchCount;
    private int turnCount;
    //public game event - set to one scene transition ready game event
    //private coremanager
    public GameEvent onSceneReady;
    private CoreManager coreManager;
    
    private void Start()
    {
        coreManager = GetCoreManager();
        if (coreManager == null)
        {
            Debug.LogError("No core detected :(");
        }
        matchCount = 0;
        turnCount = 0;
        overlayPanel.SetActive(false);
        PrepareSprites();
        CreateCards();
    }
    private void PrepareSprites()
    {
        //create duplicates for each sprite -- i.e. a match
        spritePairs = new List<Sprite>();
        for(int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        (spritePairs, textList) = Shuffle(spritePairs, textList);
    }

    //set up cards
    void CreateCards()
    {
        for(int i = 0; i < spritePairs.Count; i++)
        {
            //put on grid
            MGCardBehavior card = Instantiate(cardPrefab, gridTransform);
            card.SetFrontSprite(spritePairs[i]);
            card.SetFrontText(textList[i]);
            card.manager = this;
            card.isSelected = false;
            card.HidePanel();
        }
    }


    (List<Sprite>, List<string>) Shuffle(List<Sprite> spriteList, List<string> textList)
    {
        //Create a list of indexes
        List<int> indexList = new List<int>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            indexList.Add(i);
        }
        List<Sprite> newSpriteList = new List<Sprite>();
        List<string> newTextList = new List<string>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            int randomIndex = Random.Range(0, indexList.Count - 1);
            int newIndex = indexList[randomIndex];
            Sprite newSprite = spriteList[newIndex];
            string newText= textList[newIndex];
            newSpriteList.Add(newSprite);
            newTextList.Add(newText);
            indexList.Remove(newIndex);
        }
        return (newSpriteList, newTextList);
    }

    public void SetSelected(MGCardBehavior card)
    {
        if (!card.isSelected)
        {
            card.Show();

            if(firstSelected == null)
            {
                firstSelected = card;
                return;
            }
            if (secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatching(firstSelected, secondSelected));
                turnCount++;
                firstSelected = null;
                secondSelected = null;
            }
        }
    }

    IEnumerator CheckMatching(MGCardBehavior a, MGCardBehavior b)
    {
        yield return new WaitForSeconds(0.3f);
        if (a.frontSprite == b.frontSprite)
        {
            matchCount++;
            a.ShowPanel();
            b.ShowPanel();
            if(matchCount>= spritePairs.Count / 2)
            {
                //end game
                scoreText.text = "Score: " + turnCount;
                overlayPanel.SetActive(true);
                onSceneReady.Raise();
                //onscenetransition
                //coremanager loadnewscene - go back to home scene
                
            }
        }
        else
        {
            a.Hide();
            b.Hide();
        }
    }

    public void EndGame()
    {
        coreManager.LoadNewScene("HomeScreen");
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
