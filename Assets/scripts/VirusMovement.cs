using UnityEngine;
using TMPro;

/// <summary>
/// Movement and rotation of the virus particles.
/// </summary>
public class VirusMovement : MonoBehaviour
{
    private const float defaultLength = 5;
               
    [SerializeField] private GameObject virusBody;
    [SerializeField] private GameObject wordText;
    [SerializeField] private ParticleSystem destroyParticles;

    [SerializeField] private float speed = 0.05f;         //default movement speed
    [SerializeField] private int wordLength;

    private Vector3 destination;        //vector to travel

    private float[] rotations = new float[2];

    private float startingDistance;    
    private Vector3 startingScale;      

    // Start is called before the first frame update
    private void Start()
    {
        wordLength = wordText.GetComponent<TextMeshProUGUI>().text.Length;
        speed *= (defaultLength / wordLength);

        destination = -(transform.position - Camera.main.transform.position);
        
        //how fast will the virus rotate based on the distance from center of screen
        rotations[0] = transform.position.x / 5;
        rotations[1] = -transform.position.y / 5;

        startingDistance = Vector3.Distance(Camera.main.transform.position, transform.position);     //distance from camera
        startingScale = transform.localScale;                                                        //starting scale of tmp component
    }   

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.z <= Camera.main.transform.position.z + 3)
        {
            FindObjectOfType<GameManager>().EndGame();                       
            Destroy(gameObject);          
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(destination.x * Time.fixedDeltaTime * speed,
                            destination.y * Time.fixedDeltaTime * speed,
                            destination.z * Time.fixedDeltaTime * speed);   //movement
        
        float currentDistance = Vector3.Distance(Camera.main.transform.position, transform.position) / startingDistance;    //(0;1>
        wordText.transform.localScale = startingScale * currentDistance;    //changing TMP scale to look the same on the screen

        virusBody.transform.Rotate(rotations[1], rotations[0], 0);
    }

    private void OnDestroy()
    {
        AudioManager.Instance.Play("VirusPop");
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
    }
}
