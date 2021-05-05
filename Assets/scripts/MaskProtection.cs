using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskProtection : MonoBehaviour
{

    [SerializeField] private Slider wearTimeSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private const float MAX_WEAR_TIME = 60f;

    private int health = 3;
    private float wearTime;

    private void Start()
    {
        healthText.text = health.ToString();
        wearTimeSlider.maxValue = MAX_WEAR_TIME;
        wearTimeSlider.value = MAX_WEAR_TIME;
    }

    private void Update()
    {
        wearTime += Time.deltaTime;
        wearTimeSlider.value = MAX_WEAR_TIME - wearTime;
        if (wearTime >= MAX_WEAR_TIME)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Virus")) //colliding object is a virus
        {
            health--;
            healthText.text = health.ToString();
            WordManager wordManager = GameObject.FindGameObjectWithTag("WordManager").GetComponent<WordManager>();
            string word = other.gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
            wordManager.RemoveWordDesrtoyedByMask(word);    //so that it doesn't stay in wordList
            Destroy(other.gameObject);  //destroys the virus
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
