using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveRobot : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;

    //todo
    // [] adjust motor approximation to not need feedback
    // [] add controller support
    // [] create teleop disabled and autonomous code blocks
    // [] actually simulate swerve drive modules
    // [] centralize robot controll in this file
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //velocity = velocity * 10;
    }

    public float motorAproximation(float InputVoltage) {
        float gearRatio = 1/13.5f;  //gear ratio 1/12 = 12 to 1 gear ratio(reduction).
        float wheelRadius = 0.076f; //in meters
        
        float wheelRpm = (float)Math.Clamp(60 * (Math.Abs(rb.velocity.magnitude)/(2*Math.PI*wheelRadius)),0,380); //determine driven wheel rpm, in this situation think caster wheel in center of bot
        float motorRPM = wheelRpm/gearRatio; //determine motor shaft rpm

        float torque = (float)((-2.6/6000f * Math.Abs(motorRPM) + 2.6f) * InputVoltage); //rough torque line for a Neo
        float adjtorque = (float)((torque*gearRatio)*(4/1.8));//multiply by number of motors divided by efficiency of each extra motor

        return adjtorque/wheelRadius;//divide by wheel radius in meters

        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
