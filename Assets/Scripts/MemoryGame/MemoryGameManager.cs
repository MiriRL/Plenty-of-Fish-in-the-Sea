using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;



//using tutorial: https://www.youtube.com/watch?v=HajiNmv4UNY
public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] MGCardBehavior cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject overlayPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    private List<Sprite> spritePairs;

    MGCardBehavior firstSelected;
    MGCardBehavior secondSelected;
    int matchCount;
    int turnCount;

    private void Start()
    {
        matchCount = 0;
        turnCount = 0;
        overlayPanel.SetActive(false);
        PrepareSprites();
        CreateCards();
    }
    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();
        for(int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        spritePairs = Shuffle(spritePairs);
    }

    void CreateCards()
    {
        for(int i = 0; i < spritePairs.Count; i++)
        {
            MGCardBehavior card = Instantiate(cardPrefab, gridTransform);
            card.SetFrontSprite(spritePairs[i]);
            card.manager = this;
        }
    }


    List<Sprite> Shuffle(List<Sprite> spriteList)
    {
        //Create a list of indexes
        List<int> indexList = new List<int>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            indexList.Add(i);
        }
        List<Sprite> newSpriteList = new List<Sprite>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            int randomIndex = Random.Range(0, indexList.Count - 1);
            int newIndex = indexList[randomIndex];
            Sprite newSprite = spriteList[newIndex];
            newSpriteList.Add(newSprite);
            indexList.Remove(newIndex);
        }
        return newSpriteList;
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
            if(matchCount>= spritePairs.Count / 2)
            {
                //end game
                scoreText.text = "Score: " + turnCount;
                overlayPanel.SetActive(true);
                
            }
        }
        else
        {
            a.Hide();
            b.Hide();
        }
    }
}
