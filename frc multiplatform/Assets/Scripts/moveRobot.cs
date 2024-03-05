using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class moveRobot : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    public HingeMovement arm = new HingeMovement();
    public HingeMovement intake = new HingeMovement();
    public SlideJoint armsec1 = new SlideJoint();
    public SlideJoint armsec2 = new SlideJoint();
    public IntakeWheels coneIntake = new IntakeWheels();
    public IntakeWheels topIntake = new IntakeWheels();
    public IntakeWheels bottomeIntake = new IntakeWheels();
    private float  gearRatio = 1/8.6f;  //gear ratio 1/13.5 = 13.5 to 1 gear ratio(reduction).

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
        
        float wheelRadius = 0.0508f; //in meters
        
        float wheelRpm = (float)Math.Clamp(60 * (Math.Abs(rb.velocity.magnitude)/(2*Math.PI*wheelRadius)),0,380); //determine driven wheel rpm, in this situation think caster wheel in center of bot
        float motorRPM = wheelRpm/gearRatio; //determine motor shaft rpm

        float torque = (float)((-2.6/6000f * Math.Abs(motorRPM) + 2.6f) * Math.Clamp(InputVoltage,-1,1)); //rough torque line for a Neo
        float adjtorque = (float)((torque*gearRatio)*(4/1.8));//multiply by number of motors divided by efficiency of each extra motor

        return adjtorque/wheelRadius;//divide by wheel radius in meters

        
    }

    // Update is called once per frame
    void Update()
    {

        //Drive Train stuff\
        
        rb.maxLinearVelocity = (float)((0.0508 * ((2* Math.PI * (5676 * gearRatio))/60)));//determine max linear speed using the the tip speed of the drive wheels when running at full speed, 5676 is neo 1.1 emperical free speed
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
            rb.AddRelativeTorque(0,40,0);
        }
        if (Input.GetKey("q"))
        {
            rb.AddRelativeTorque(0,-40,0);
        }

    //Arm Stuff

        arm.AxisVector = new Vector3(0,0,1);
        intake.AxisVector = new Vector3(1,0,0);
        
        if(Input.GetKey("r")){
            arm.TargetAngle = 0;
            intake.TargetAngle = -102;
            armsec2.targetDistance = 0.1f;
            armsec1.targetDistance = 0.0f;
        }

        if(Input.GetKey("f")) {
            arm.TargetAngle = 0;
            intake.TargetAngle = 0;
            armsec2.targetDistance = 0.0f;
            armsec1.targetDistance = 0.0f;
        }

        if(Input.GetKey("c")){
            arm.TargetAngle = 120;
            intake.TargetAngle = -15;
            armsec2.targetDistance = 0.4f;
            armsec1.targetDistance = 0.28f;
        }

        if(Input.GetKey("1")){
            coneIntake.speed = 0;
            topIntake.speed = 3000;
            bottomeIntake.speed = -3000;
        }

        if (Input.GetKey("2")) {
            coneIntake.speed = 4000;
            topIntake.speed = -4000;
            bottomeIntake.speed = 4000;
        }
    }
}
