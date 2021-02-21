using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOver;

    bool gameEnded = false;
    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            GameOver.SetActive(true);
            Invoke("Restart", 4);
        }       
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
