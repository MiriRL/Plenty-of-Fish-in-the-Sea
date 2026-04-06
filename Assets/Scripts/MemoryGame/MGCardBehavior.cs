using UnityEngine;
using UnityEngine.UI;



public class MGCardBehavior : MonoBehaviour
{
    [SerializeField] private Image frontImg;
    public Sprite hiddenFrontSprite;
    public Sprite frontSprite;

    public bool isSelected;
    public MemoryGameManager manager;

    public void OnCardClick()
    {
        manager.SetSelected(this);
    }
    public void SetFrontSprite(Sprite s)
    {
        frontSprite = s;
    }

    public void Show()
    {   
        frontImg.sprite = frontSprite;
        isSelected = true;
    }

    public void Hide()
    {
       frontImg.sprite = hiddenFrontSprite;
        isSelected = false;
    }
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//  s   {
//         frontRenderer = cardInput.face.GetComponent<SpriteRenderer>();
//         backRenderer = cardInput.back.GetComponent<SpriteRenderer>();
//         backRenderer.enabled = true;
//         //enable backRenderer
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void onClick()
//     {
//         if (cardInput.flippable)
//         {
//             backRenderer.enabled = false;
//             frontRenderer.enabled = true;
//             cardInput.flippable = false;
//             cardInput.flipEvent.Raise();
//         }
//     }

//     public void FlipBack()
//     {
//         frontRenderer.enabled = false;
//         backRenderer.enabled = true; 
//         cardInput.flippable = true;
//     }

//     public bool CheckMatch(MemoryGameCard otherCard)
//     {
//       return (cardInput.match == otherCard);
//     }
// }
