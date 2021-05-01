using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button play;
    private void OnEnable()
    {
        play.Select();
    }

    public void PlayGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("Endless");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
        
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        PlayerPrefs.Save();
        Application.Quit();
    }
}
