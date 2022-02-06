using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Management starting / restarting of the game.
/// </summary>
public class PlayQuitMenu : MonoBehaviour
{
    [SerializeField] private Button play;
    private void OnEnable()
    {
        play.Select();
    }

    /// <summary>
    /// Starts a new game.
    /// </summary>
    public void PlayGame()
    {
        GameManager.UploadError = false;
        if (SceneManager.GetActiveScene().name == "Menu") //checking if current scene is Menu or GameOver
        {
            SceneManager.LoadScene("Endless");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
        
    }

    /// <summary>
    /// Exit the game.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("quit");
        PlayerPrefs.Save();
        Application.Quit();
    }
}
