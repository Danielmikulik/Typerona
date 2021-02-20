using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject VirusBody;
    public WordDisplay SpawnWord()
    {
        float x = Random.Range(-15f, 15f);
        float y = Random.Range(-4f, 4f);
        float z = 5f;
        GameObject virusObj = Instantiate(VirusBody, new Vector3(x, y, z), Quaternion.identity);
        WordDisplay wordDisplay = virusObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
