using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOver;
    public WordManager wordManager;

    bool gameEnded = false;
    public void EndGame()
    {
        if (!gameEnded)
        {
            wordManager.writeStats();
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
