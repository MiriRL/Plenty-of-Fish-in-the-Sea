using UnityEngine;
using System.Collections.Generic;
using System.Collections;



//using tutorial: https://www.youtube.com/watch?v=HajiNmv4UNY -- 4:59
public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] MGCardBehavior cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;
    private List<Sprite> spritePairs;

    MGCardBehavior firstSelected;
    MGCardBehavior secondSelected;
    int matchCount;


    private void Start()
    {
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
        Shuffle(spritePairs);
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

    void Shuffle(List<Sprite> spriteList)
    {
        for (int i = spriteList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Sprite temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
        }
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
            }
        }
        else
        {
            a.Hide();
            b.Hide();
        }
    }
    // [SerializeField] List<GameObject> cardList;
    // private List<GameObject> pendingCards;
    // private List<GameObject> matchedCards;
    // private int turnCount;
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     // initialize global variables
    //     turnCount = 0;
    //     pendingCards = new List<GameObject>();
    //     matchedCards = new List<GameObject>();
    //     // shuffle the cards
    //     cardList.Sort((x,y) => RandomSort());
    //     //render cards on the screen - the backs should be showing
    // }

    // // Update is called once per frame
    // void Update()
    // {
     
    // }

    // // this function just returns a number in the range -1 to +1
    // // and is used by Array.Sort to 'shuffle' the array
    // //sourced from https://discussions.unity.com/t/random-shuffle-array/443149/2
    // int RandomSort()
    // {
    //     return Random.Range(-1, 2);
    // }

    // private void HandleMatch()
    // {
    //     matchedCards.Add(pendingCards[0]);
    //     matchedCards.Add(pendingCards[1]);
    //     pendingCards.Clear();
    //     if (matchedCards.Count == cardList.Count)
    //     {
    //         HandleWin();
    //     }
    // }

    // private void HandleWin()
    // {
    //     // translate turn count into a score
    //     // end game
    // }

    // public void onEvent()
    // {
    //      //Listen for card flips  
    //   // when a card is clicked, add it to the pendingCards array

    //   //When two cards have been flipped
    //   if (pendingCards.Count == 2)
    //     {
    //         turnCount = turnCount + 1;
    //         // check if they match!
    //         MGCardBehavior card0 = pendingCards[0].GetComponent<MGCardBehavior>();
    //         MGCardBehavior card1 = pendingCards[1].GetComponent<MGCardBehavior>();
    //         if (card0.CheckMatch(card1.cardInput))
    //         {
    //             HandleMatch();
    //         }
    //         else
    //         {
    //            card0.FlipBack();
    //            card1.FlipBack();
    //             pendingCards.Clear();
    //         }
    //     }
    // }
}
