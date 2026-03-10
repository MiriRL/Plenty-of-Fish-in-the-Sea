using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartClick()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }
}
