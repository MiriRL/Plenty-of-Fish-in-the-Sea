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

    // Game State
    private List<Character> knownChars = new List<Character>();
    
    // This should only be loaded in once, after the start screen. When it is, we want to go straight to the 
    //      home screen with the intro dialogue
    void Start()
    {
        LoadNewScene("HomeScreen");
    }

    public List<Character> GetKnownCharacters()
    {
        return knownChars;
    }

    // Add a new character to the known characters list
    public void UpdateKnownCharacters(Character newCharacter)
    {
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
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncUnload.isDone)
        {
            yield return null;
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

    
}
