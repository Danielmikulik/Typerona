using UnityEngine;

public class MaskProtection : MonoBehaviour
{
    private int health = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Virus")
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
