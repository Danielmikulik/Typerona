using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, particles.main.duration);
    }
}
