using UnityEngine;
using TMPro;

public class VirusMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.2f;         //movement speed
    private Vector3 destination;        //destination point
    public Rigidbody rb;                //sphere
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        destination = -(transform.position - Camera.main.transform.position);
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

        //transform.Rotate(1,1,1);
        //text.rectTransform.eulerAngles = new Vector3(0, 0, 0);
    }
}
