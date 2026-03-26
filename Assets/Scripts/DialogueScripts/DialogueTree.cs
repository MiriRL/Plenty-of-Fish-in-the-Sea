/*
Base code file taken from in-class example, written by Bret Jackson.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewDialogue", menuName ="Dialogue/Dialogue Tree")]
public class DialogueTree : ScriptableObject
{
    public Character character;
    public Sentence startingSentence;
    [Tooltip("The number of hearts associated with this dialogue tree.")]
    public int requiredHearts;
}