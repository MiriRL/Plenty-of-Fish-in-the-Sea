using UnityEngine;

[CreateAssetMenu(fileName = "MemoryGameCard", menuName = "Scriptable Objects/MemoryGameCard")]
public class MemoryGameCard : ScriptableObject
{
  //the matching card to this card
  public MemoryGameCard match;
  public GameObject face;
  public GameObject back;
  public bool flippable;
  public GameEvent flipEvent;
    
}
