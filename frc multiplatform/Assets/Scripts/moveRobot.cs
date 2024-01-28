using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveRobot : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    public HingeMovement arm = new HingeMovement();
    public HingeMovement intake = new HingeMovement();

    //todo
    // [] adjust motor approximation to not need feedback
    // [] add controller support
    // [] create teleop disabled and autonomous code blocks
    // [] actually simulate swerve drive modules
    // [in progress] centralize robot controll in this file
    

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

        //Drive Train stuff

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

        if (Input.GetKey("e"))
        {
            rb.AddRelativeTorque(0,80,0);
        }
        if (Input.GetKey("q"))
        {
            rb.AddRelativeTorque(0,-80,0);
        }

    //Arm Stuff

        arm.AxisVector = new Vector3(0,0,1);
        intake.AxisVector = new Vector3(1,0,0);
        
        if(Input.GetKey("r")){
            arm.TargetAngle = -5;
            intake.TargetAngle = -58;
        }
    }
}
