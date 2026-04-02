using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject phone;

    public void ToggleMenu()
    {
        phone.SetActive(!phone.activeSelf);
    }
}
