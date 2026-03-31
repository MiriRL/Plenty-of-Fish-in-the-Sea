using UnityEngine;
using System.Collections.Generic;
public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cardList;
    private List<GameObject> pendingCards;
    private List<GameObject> matchedCards;
    private int turnCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initialize global variables
        turnCount = 0;
        pendingCards = new List<GameObject>();
        matchedCards = new List<GameObject>();
        // shuffle the cards
        cardList.Sort((x,y) => RandomSort());
        //render cards on the screen - the backs should be showing
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    // this function just returns a number in the range -1 to +1
    // and is used by Array.Sort to 'shuffle' the array
    //sourced from https://discussions.unity.com/t/random-shuffle-array/443149/2
    int RandomSort()
    {
        return Random.Range(-1, 2);
    }

    private void HandleMatch()
    {
        matchedCards.Add(pendingCards[0]);
        matchedCards.Add(pendingCards[1]);
        pendingCards.Clear();
        if (matchedCards.Count == cardList.Count)
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        // translate turn count into a score
        // end game
    }

    public void onEvent()
    {
         //Listen for card flips  
      // when a card is clicked, add it to the pendingCards array

      //When two cards have been flipped
      if (pendingCards.Count == 2)
        {
            turnCount = turnCount + 1;
            // check if they match!
            MGCardBehavior card0 = pendingCards[0].GetComponent<MGCardBehavior>();
            MGCardBehavior card1 = pendingCards[1].GetComponent<MGCardBehavior>();
            if (card0.CheckMatch(card1.cardInput))
            {
                HandleMatch();
            }
            else
            {
               card0.FlipBack();
               card1.FlipBack();
                pendingCards.Clear();
            }
        }
    }
}
