using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public float motorAproximation(float InputVoltage) {
        float gearRatio = 1/13.5f;  //gear ratio 1/12 = 12 to 1 gear ratio(reduction).
        
        float wheelRpm = (float)Math.Clamp(60 * (Math.Abs(rb.velocity.magnitude)/(2*Math.PI*0.076)),0,380);
        float motorRPM = wheelRpm/gearRatio;

        float torque = (float)((-2.6/6000f * Math.Abs(motorRPM) + 2.6f) * InputVoltage);
        float adjtorque = (float)((torque*gearRatio)*(4/1.8));

        return (adjtorque/0.076f);//divide by wheel radius in meters

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((float)Math.Clamp(60 * (Math.Abs(rb.velocity.magnitude)/(2*Math.PI*0.076)),0,380));
        //Debug.Log(motorAproximation(1));
        //Debug.Log(Input.GetKey("s"));
        if (Input.GetKey("s"))
        {
            rb.AddRelativeForce(Vector3.left * motorAproximation(1), ForceMode.Acceleration);
        }
        if (Input.GetKey("w"))
        {
            rb.AddRelativeForce(Vector3.right * motorAproximation(1), ForceMode.Acceleration);
        }
        if (Input.GetKey("d"))
        {
            rb.AddRelativeForce(Vector3.back * motorAproximation(1), ForceMode.Acceleration);
        }
        if (Input.GetKey("a"))
        {
            rb.AddRelativeForce(Vector3.forward * motorAproximation(1), ForceMode.Acceleration);
        }

        if (Input.GetKey("l"))
        {
            rb.AddRelativeTorque(0,80,0);
        }
        if (Input.GetKey("j"))
        {
            rb.AddRelativeTorque(0,-80,0);
        }

        if (this.transform.position.y < -1) 
        {
            this.transform.position = new Vector3(0, 3, 0);
        }
    }
}
