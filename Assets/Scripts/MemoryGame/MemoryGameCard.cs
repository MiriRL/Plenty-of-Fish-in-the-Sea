using UnityEngine;

[CreateAssetMenu(fileName = "MemoryGameCard", menuName = "Scriptable Objects/MemoryGameCard")]
public class MemoryGameCard : ScriptableObject
{
    //represents the text on the card
  [SerializeField] string text; 
  //the matching card to this card
  [SerializeField] MemoryGameCard match;
  [SerializeField] Sprite face;
  [SerializeField] Sprite back;
  public bool flippable;

  //Front utilizes the text - 

    //onClick:
    //if flippable:
      //flip to front face
      // display text
      // flippable = false
      // send a signal to MemoryGameManager

    //public void FlipBack(){
      //remove text
      //flip to back face 
      // set flippable to true
    //}
    

    public bool CheckMatch(MemoryGameCard otherCard)
    {
      return (match == otherCard);
    }
    
}
