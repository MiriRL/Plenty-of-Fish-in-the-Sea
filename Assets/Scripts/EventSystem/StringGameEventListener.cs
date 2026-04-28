/*
Base code file taken from in-class example, written by Bret Jackson.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringGameEventListener : MonoBehaviour
{
    public StringGameEvent gameEvent;
    public UnityEvent<string> responseEvent;

    private void OnEnable(){
        gameEvent.RegisterListener(this);
    }

    private void OnDisable(){
        gameEvent.UnregisterListener(this);
    }

    public void RaiseEvent(string value){
        responseEvent.Invoke(value);
    }
}