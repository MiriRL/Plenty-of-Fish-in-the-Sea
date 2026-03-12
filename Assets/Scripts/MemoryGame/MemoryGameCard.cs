using UnityEngine;

[CreateAssetMenu(fileName = "MemoryGameCard", menuName = "Scriptable Objects/MemoryGameCard")]
public class MemoryGameCard : ScriptableObject
{
    //represents the text on the card
  [SerializeField] string text; 
  //the matching card to this card
  [SerializeField] MemoryGameCard match;
  public bool flippable;

  //Store card faces in a List - what type would the card faces be?
  //Front utilizes the text - 

    //onClick:
    //if flippable:
      //flip to front face
      // display text
      // send a signal to MemoryGameManager

    //public void FlipBack(){
      //remove text
      //flip to back face 
    //}
    
    
}
