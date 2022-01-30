using UnityEngine;

/// <summary>
/// A script responsible for destroying particles after the time of their animation.
/// </summary>
public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, particles.main.duration);   //destroys particle gameObject after the animation
    }
}
