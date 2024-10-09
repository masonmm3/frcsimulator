using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class moveRobot : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    public HingeMovement arm;
    public HingeMovement intake;
    public SlideJoint armsec1;
    public SlideJoint armsec2;
    public IntakeWheels coneIntake;
    public IntakeWheels topIntake;
    public IntakeWheels bottomeIntake;
    public SwerveWheel[] swerveWheels;
    private float gearRatio = 1 /8.6f;

    private Vector2 translateValue;
    private Vector2 rotateValue;
    private Vector3 driveInput;

    private bool onLow;
    private bool onMid;
    private bool onHigh;
    private bool onStation;

    private bool Low;
    private bool Mid;
    private bool High;
    private bool Station;
    private bool debounce;

    private bool onIntake;
    private bool onOutake;

    private float extendTarget;

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
       

        Low = false;
        Mid = false;
        High = false;
        Station = false;

        //velocity = velocity * 10;
    }

    public void SwerveSetpoints()
    {
        driveInput = new Vector3(translateValue.y, 0, translateValue.x);

        Vector3 rotatedVector = Quaternion.AngleAxis(-90, Vector3.up) * driveInput;

        float FWD, STR;

        FWD = rotatedVector.x;

        STR = rotatedVector.z;

        float RCW = -rotateValue.x;

        float L = swerveWheels[1].Position.x + swerveWheels[2].Position.x;

        float W = swerveWheels[0].Position.z + swerveWheels[1].Position.z;

        float R = Mathf.Sqrt(MathF.Pow(L, 2) + Mathf.Pow(W, 2));

        float A = STR - RCW * (L / R);
        float B = STR + RCW * (L / R);
        float C = FWD - RCW * (W / R);
        float D = FWD + RCW * (W / R);

        float ws1 = Mathf.Sqrt(Mathf.Pow(B, 2) + Mathf.Pow(C, 2));
        float wa1 = Mathf.Atan2(B, C) * 180 / Mathf.PI;

        float ws2 = Mathf.Sqrt(Mathf.Pow(B, 2) + Mathf.Pow(D, 2));
        float wa2 = Mathf.Atan2(B, D) * 180 / Mathf.PI;

        float ws3 = Mathf.Sqrt(Mathf.Pow(A, 2) + Mathf.Pow(D, 2));
        float wa3 = Mathf.Atan2(A, D) * 180 / Mathf.PI;

        float ws4 = Mathf.Sqrt(Mathf.Pow(A, 2) + Mathf.Pow(C, 2));
        float wa4 = Mathf.Atan2(A, C) * 180 / Mathf.PI;

        swerveWheels[0].WA = wa1;
        swerveWheels[1].WA = wa2;
        swerveWheels[3].WA = wa3;
        swerveWheels[2].WA = wa4;

        swerveWheels[0].WS = ws1 * velocity;
        swerveWheels[1].WS = ws2 * velocity;
        swerveWheels[3].WS = ws3 * velocity;
        swerveWheels[2].WS = ws4 * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        

       
    //Arm Stuff

        arm.AxisVector = new Vector3(0,0,1);
        intake.AxisVector = new Vector3(0,0,1);
        armsec1.AxisVector = new Vector3(1,0,0);
        armsec2.AxisVector = new Vector3(0,1,0);

        if (onLow && !debounce)
        {
            Low = !Low;
            Mid = false;
            High = false;
            Station = false;
        }

        if (onMid && !debounce)
        {
            Mid = !Mid;
            Low = false;
            High = false;
            Station = false;
        }

        if (onHigh && !debounce)
        {
            High = !High;
            Mid = false;
            Low = false;
            Station = false;
        }

        if (onStation && !debounce)
        {
            Station = !Station;
            Mid = false;
            High = false;
            Low = false;
        }

        if (onLow || onMid | onHigh || onStation)
        {
            debounce = true;
        } else
        {
            debounce = false;
        }

        if (Low)
        {
            arm.TargetAngle = 0;
            intake.TargetAngle = 108;
            extendTarget = 0.12f;
        }
        else if (Mid)
        {
            arm.TargetAngle = -140;
            intake.TargetAngle = 10;
            extendTarget = 0.53f;
        }
        else if (High) 
        {
            arm.TargetAngle = -140;
            intake.TargetAngle = 24.5f;
            extendTarget = 0.99f;
        } else if (Station)
        {
            arm.TargetAngle = -108f;
            intake.TargetAngle = -5;
            extendTarget = 0.56f;
        } else
        {
            arm.TargetAngle = 0;
            intake.TargetAngle = 0;
            extendTarget = 0;
        }
        if (extendTarget > 0 && arm.TargetAngle != 0)
        {
            extendTarget = extendTarget * Mathf.Abs(arm.gameObject.transform.localRotation.eulerAngles.z / arm.TargetAngle);
        }

        armsec1.targetDistance = -extendTarget / 2;
        armsec2.targetDistance = -extendTarget / 2; 


        if(onIntake){
            coneIntake.speed = 4000;
            topIntake.speed = -4000;
            bottomeIntake.speed = 4000;
        }

        if (onOutake) {
            coneIntake.speed = -4000;
            topIntake.speed = 4000;
            bottomeIntake.speed = -4000;
        }


        

        //Drive Train Stuff

        float L = swerveWheels[1].Position.x + swerveWheels[2].Position.x;

        float W = swerveWheels[0].Position.z + swerveWheels[1].Position.z;

        float R = Mathf.Sqrt(MathF.Pow(L, 2) + Mathf.Pow(W, 2));

        SwerveSetpoints();

        rb.maxLinearVelocity = (0.0508f * ((2 * Mathf.PI * (5676 * gearRatio)) / 60));//determine max linear speed using the the tip speed of the drive wheels when running at full speed, 5676 is neo 1.1 emperical free speed
        rb.maxAngularVelocity = 2 * Mathf.PI * R * (0.0508f * ((2 * Mathf.PI * (5676 * gearRatio)) / 60)); ;

    }



    public void OnTranslate(InputAction.CallbackContext obj)
    {
        translateValue = obj.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext obj)
    {
        rotateValue = obj.ReadValue<Vector2>();
    }

    public void OnLow(InputAction.CallbackContext obj)
    {
        onLow = obj.action.triggered;
    }

    public void OnMid(InputAction.CallbackContext obj)
    {
        onMid = obj.action.triggered;
    }

    public void OnHigh(InputAction.CallbackContext obj)
    {
        onHigh = obj.action.triggered;
    }

    public void OnStation(InputAction.CallbackContext obj)
    {
        onStation = obj.action.triggered;
    }

    public void OnOutake(InputAction.CallbackContext obj)
    {
        onOutake = obj.action.triggered;
    }

    public void OnIntake(InputAction.CallbackContext obj)
    {
        onIntake = obj.action.triggered;
    }




}
