using TMPro;
using UnityEngine;

public class MaskProtection : MonoBehaviour
{
    private const float MAX_WEAR_TIME = 25f;

    private int health = 3;
    private float wearTime;
   

    private void Update()
    {
        wearTime += Time.deltaTime;
        if (wearTime >= MAX_WEAR_TIME)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Virus")
        {
            health--;
            WordManager wordManager = GameObject.FindGameObjectWithTag("WordManager").GetComponent<WordManager>();
            string word = other.gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
            wordManager.RemoveWordDesrtoyedByMask(word);
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
