using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button play;
    public void Start()
    {
        play.Select();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Endless");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }


}
