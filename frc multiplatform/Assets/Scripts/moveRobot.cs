using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRobot : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //velocity = velocity * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("s"))
        {
            rb.AddRelativeForce(Vector3.left * velocity);
        }
        if (Input.GetKey("w"))
        {
            rb.AddRelativeForce(Vector3.right * velocity);
        }
        if (Input.GetKey("d"))
        {
            rb.AddRelativeForce(Vector3.back * velocity);
        }
        if (Input.GetKey("a"))
        {
            rb.AddRelativeForce(Vector3.forward * velocity);
        }

        if (Input.GetKey("l"))
        {
            this.transform.Rotate(0.0f, 0.2f, 0.0f);
        }
        if (Input.GetKey("j"))
        {
            this.transform.Rotate(0.0f, -0.2f, 0.0f);
        }

        if (this.transform.position.y < -1) 
        {
            this.transform.position = new Vector3(0, 3, 0);
        }
    }
}
