/*
Base code file taken from in-class example, written by Bret Jackson. Edited for passing strings.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StringGameEvent", menuName ="Events/String Game Event")]
public class StringGameEvent : ScriptableObject
{
    private readonly List<StringGameEventListener> listeners = new List<StringGameEventListener>();

    public void RegisterListener(StringGameEventListener listener){
        if (!listeners.Contains(listener)){
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(StringGameEventListener listener){
        listeners.Remove(listener);
    }

    public void Raise(string value){
        for (int i = listeners.Count -1 ; i >= 0; i--){
            listeners[i].RaiseEvent(value);
        }
    }
}