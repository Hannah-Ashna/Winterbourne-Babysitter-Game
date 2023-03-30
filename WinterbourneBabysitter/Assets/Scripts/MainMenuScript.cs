using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        try
        {
            Destroy(GameObject.Find("PlayerInventory"));
        }
        catch
        {

        }
        SceneManager.LoadScene(1);
    }

    public void ReplayGame() {
        try
        {
            Destroy(FindObjectOfType<PlayerInventory>());
        }
        catch {
        
        }
        SceneManager.LoadScene(1);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OpenURL()
    {
        // Change link later
        Application.OpenURL("https://theconversation.com/chalk-streams-why-englands-rainforests-are-so-rare-and-precious-172827");
    }
}
