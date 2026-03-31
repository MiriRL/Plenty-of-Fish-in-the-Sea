using UnityEngine;




public class MGCardBehavior : MonoBehaviour
{

    //every card asset has an attached renderer
    private SpriteRenderer frontRenderer;
    private SpriteRenderer backRenderer;

    public MemoryGameCard cardInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frontRenderer = cardInput.face.GetComponent<SpriteRenderer>();
        backRenderer = cardInput.back.GetComponent<SpriteRenderer>();
        backRenderer.enabled = true;
        //enable backRenderer
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        if (cardInput.flippable)
        {
            backRenderer.enabled = false;
            frontRenderer.enabled = true;
            cardInput.flippable = false;
            cardInput.flipEvent.Raise();
        }
    }

    public void FlipBack()
    {
        frontRenderer.enabled = false;
        backRenderer.enabled = true; 
        cardInput.flippable = true;
    }

    public bool CheckMatch(MemoryGameCard otherCard)
    {
      return (cardInput.match == otherCard);
    }
}
