using UnityEngine;
using System.Collections.Generic;
public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] List<MemoryGameCard> cardList;
    private List<MemoryGameCard> pendingCards;
    private List<MemoryGameCard> matchedCards;
    private int turnCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initialize global variables
        turnCount = 0;
        pendingCards = new List<MemoryGameCard>();
        matchedCards = new List<MemoryGameCard>();
        // shuffle the cards
        //System.Array.Sort(cardList, RandomSort);
    }

    // Update is called once per frame
    void Update()
    {
      //Listen for card flips  
      // when a card is clicked, add it to the pendingCards array

      //When two cards have been flipped
      if (pendingCards.Count == 2)
        {
            turnCount = turnCount + 1;
            // check if they match!
            if (pendingCards[0].CheckMatch(pendingCards[1]))
            {
                HandleMatch();
            }
            else
            {
                // pendingCards[0].FlipBack();
                // pendingCards[1].FlipBack();
                pendingCards.Clear();
            }
        }
    }

    // this function just returns a number in the range -1 to +1
    // and is used by Array.Sort to 'shuffle' the array
    //sourced from https://discussions.unity.com/t/random-shuffle-array/443149/2
    int RandomSort(string a, string b)
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
}
