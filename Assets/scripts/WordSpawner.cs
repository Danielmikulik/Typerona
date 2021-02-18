using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject VirusBody;
    public WordDisplay SpawnWord()
    {
        int x = Random.Range(-10, 10);
        int y = Random.Range(-3, 3);
        int z = 5;
        GameObject virusObj = Instantiate(VirusBody, new Vector3(x,y,z), Quaternion.identity);
        VirusWordSpawner spawner = virusObj.GetComponent<VirusWordSpawner>();
        WordDisplay wordDisplay = spawner.GetWordPrefab().GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
