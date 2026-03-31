using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterSpriteController : MonoBehaviour
{
    public Character talkingCharacter;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void UpdateSprite()
    {
        spriteRenderer.sprite = talkingCharacter.currentEmotion.emotionSprite;
    }

    public void ChangeCharacter(Character newCharacter)
    {
        if (newCharacter != talkingCharacter)
        {
            talkingCharacter = newCharacter;
            UpdateSprite();
        }
    }
}
