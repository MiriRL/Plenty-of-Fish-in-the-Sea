using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoBehaviour
{
    // This empty core scene will sit below other scenes after the start button is clicked. 
    // It should remain in the background and other scenes should be ADDITIVE loaded on top.
    // Currently made to hold only one other top scene.
    private string currTopScene = null;
    [Tooltip("The first character the player has access to.")]
    [SerializeField] private Character firstCharacter;
    private bool ready = false;  // Tracks whether the game is ready to transition scenes

    // Game State
    private List<Character> knownChars = new List<Character>();
    [NonSerialized] public Character currentCharacter;
    [NonSerialized] public int minigameScore;
    
    // This should only be loaded in once, after the start screen. When it is, we want to go straight to the 
    //      home screen with the intro dialogue (Mom)
    void Start()
    {
        knownChars.Add(firstCharacter);
        LoadNewScene("HomeScreen");
    }

    public List<Character> GetKnownCharacters()
    {
        return knownChars;
    }

    // Add a new character to the known characters list
    public void UpdateKnownCharacters(Character newCharacter)
    {
        if (knownChars.Contains(newCharacter))
        {
            // No duplicates
            return;
        }
        knownChars.Add(newCharacter);
    }
    public void LoadNewScene(string sceneName)
    {
        if (currTopScene != null)
        {
            StartCoroutine(UnloadRoutine(currTopScene));
        }
        StartCoroutine(LoadNewRoutine(sceneName));
    }

    private IEnumerator UnloadRoutine(string sceneName)
    {
        ready = false;
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncUnload.isDone)
        {
            yield return new WaitUntil(() => ready);
        }
    }

    private IEnumerator LoadNewRoutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            // TODO: Add a scene transition here
            yield return null;
        }
        currTopScene = sceneName;
    }

    public void SetReady()
    {
        ready = true;
    }
    
}
