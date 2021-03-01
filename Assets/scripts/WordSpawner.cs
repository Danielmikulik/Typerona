using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject Virus;
    public WordDisplay SpawnWord()
    {
        float x = Random.Range(-18f, 18f);
        float y = Random.Range(-7f, 7f);
        float z = 5f;
        GameObject virusObj = Instantiate(Virus, new Vector3(x, y, z), Quaternion.identity);
        WordDisplay wordDisplay = virusObj.transform.GetChild(1).transform.GetChild(0).GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
