using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] GameObject menuContainer;


    public void OpenURL()
    {
        // Change link later
        Application.OpenURL("https://theconversation.com/chalk-streams-why-englands-rainforests-are-so-rare-and-precious-172827");
    }

    public void ReturntoMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void toggleMenu() {
        menuContainer.SetActive(!menuContainer.activeSelf);
    }
}
