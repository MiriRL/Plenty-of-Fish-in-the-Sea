using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MGCardBehavior : MonoBehaviour
{
    [SerializeField] private Image frontImg;
    private Sprite cardBack;
    public TextMeshProUGUI text;
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
    public void SetFrontText(string s)
    {
        text.text = s;
    }

    public void Show()
    {   
        frontImg.sprite = hiddenFrontSprite;
        text.enabled = true;
        isSelected = true;
    }

    public void Hide()
    {
        text.enabled = false;
        frontImg.sprite = cardBack;
        isSelected = false;
    }

    void Start()
    {
        text.enabled = false;
        cardBack = frontImg.sprite;
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
