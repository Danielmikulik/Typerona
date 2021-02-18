using UnityEngine;

public class VirusWordSpawner : MonoBehaviour
{
    public GameObject word;             //text of word
    // Start is called before the first frame update
    void Start()
    {
        this.word = Instantiate(word, transform.position, Quaternion.identity, transform);       
    }

    public GameObject GetWordPrefab()
    {
        return this.word;
    }
}
