using UnityEngine;
using TMPro;

public class VirusMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.2f;         //movement speed
    private Vector3 destination;        //destination point
    public Rigidbody rb;                
    public GameObject virusBody;
    [SerializeField]
    private float[] rotation = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        destination = -(transform.position - Camera.main.transform.position);
        rotation[0] = transform.position.x / 18;
        rotation[1] = transform.position.y / 7;
        rotation[2] = transform.position.z / 4;
    }

    // Update is called once per frame
    void Update()
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
                            destination.z * Time.fixedDeltaTime * speed);

        virusBody.transform.Rotate(rotation[0], rotation[1], rotation[2]);
    }
}
