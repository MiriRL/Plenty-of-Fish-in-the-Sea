using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public GameObject phone;
    public GameObject homeScreen;
    public GameObject fingeScreen;

    public void ToggleMenu()
    {
        phone.SetActive(!phone.activeSelf);
        
        if (phone.activeSelf)
        {
            // Always open the home screen
            homeScreen.SetActive(true);
            fingeScreen.SetActive(false);
        }
    }

    public void OpenFinge()
    {
        fingeScreen.SetActive(true);
        homeScreen.SetActive(false);
    }

    public void OpenDebugScene()
    {
        SceneManager.LoadScene("DebugSceneMenu", LoadSceneMode.Single);
    }

}
