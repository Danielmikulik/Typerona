using System;
using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.2f;         //movement speed
    private Vector3 destination;        //destination point
    public Rigidbody rb;                //sphere

    // Start is called before the first frame update
    void Start()
    {
        destination = -(transform.position - Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(destination.x * Time.fixedDeltaTime * speed,
                            destination.y * Time.fixedDeltaTime * speed,
                            destination.z * Time.fixedDeltaTime * speed);

        if (transform.position.z <= Camera.main.transform.position.z + 3)
        {
            Destroy(gameObject);
        }
    }
}
