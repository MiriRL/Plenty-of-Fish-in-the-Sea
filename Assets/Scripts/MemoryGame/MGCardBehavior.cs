using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

//Represents a singular card for the matching game!
public class MGCardBehavior : MonoBehaviour
{
    //Serializable Fields
    
    //representing the actual card Image
    [SerializeField] private Image frontImg;

    [SerializeField] private GameObject matchPanel;
    [SerializeField] private GameObject hoverPanel;
    // text displayed on the card
    public TextMeshProUGUI text;
    // front of the card
    public Sprite hiddenFrontSprite;
    // sprite (additional image) on the card -- used to identify matches, not displayed
    public Sprite frontSprite;

    public bool isSelected;
    public MemoryGameManager manager;

    private Sprite cardBack;
    

    // Tells the manager which card has been clicked
    public void OnCardClick()
    {
        manager.SetSelected(this);
        hoverPanel.SetActive(false);

    }

    //Sets the display image of the card -- only used for identifying pairs
    public void SetFrontSprite(Sprite s)
    {
        frontSprite = s;
    }

    // sets the front text of the card
    public void SetFrontText(string s)
    {
        text.text = s;
    }

    //flips the card face up
    public void Show()
    {   
        //change front sprite to front of card
        frontImg.sprite = hiddenFrontSprite;
        text.enabled = true;
        isSelected = true;
    }

    // flips the card face down
    public void Hide()
    {
        text.enabled = false;
        //changes front image to back of card
        frontImg.sprite = cardBack;
        isSelected = false;
    }

    void Start()
    {
        text.enabled = false;
        //get sprite from front image
        cardBack = frontImg.sprite;
        hoverPanel.SetActive(false);
    }

    public void HidePanel()
    {
        matchPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        matchPanel.SetActive(true);
        StartCoroutine(AnimatePanel());
    }

    IEnumerator AnimatePanel()
    {
        for( int i = 0; i <= 4; i++)
        {
             matchPanel.transform.Rotate(matchPanel.transform.rotation.x, matchPanel.transform.rotation.y, 90 * i);
            yield return new WaitForSeconds(0.5f);
        }
        HidePanel();
    }

    public void startHover()
    {
        if (!isSelected)
        {
            hoverPanel.SetActive(true);
        }
        
    }

    public void stopHover()
    {
        if (!isSelected)
        {
            hoverPanel.SetActive(false);
        }
        
    }
}