using UnityEngine;
using TMPro;

public class VirusMovement : MonoBehaviour
{
    const float defaultLength = 5;

    [SerializeField]
    private float speed = 0.1f;         //movement speed
    [SerializeField]
    private int wordLength;
    private Vector3 destination;        //destination point

    public Rigidbody rb;                
    public GameObject virusBody;
    public GameObject wordText;
    public ParticleSystem destroyParticles;
    
    private float[] rotations = new float[2];

    private float startingDistance;
    private Vector3 startingScale;

    // Start is called before the first frame update
    void Start()
    {
        wordLength = wordText.GetComponent<TextMeshProUGUI>().text.Length;
        speed = speed * (defaultLength / wordLength);

        destination = -(transform.position - Camera.main.transform.position);
        rotations[0] = transform.position.x / 5;
        rotations[1] = -transform.position.y / 5;

        startingDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        startingScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= Camera.main.transform.position.z + 3)
        {
            FindObjectOfType<GameManager>().EndGame();                       
            Destroy(gameObject, 2f);          
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(destination.x * Time.fixedDeltaTime * speed,
                            destination.y * Time.fixedDeltaTime * speed,
                            destination.z * Time.fixedDeltaTime * speed);
        
        float currentDistance = Vector3.Distance(Camera.main.transform.position, transform.position) / startingDistance;
        wordText.transform.localScale = startingScale * currentDistance;

        virusBody.transform.Rotate(rotations[1], rotations[0], 0);
    }

    private void OnDestroy()
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
    }
}
