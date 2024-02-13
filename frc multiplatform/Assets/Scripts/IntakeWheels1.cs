using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeWheels : MonoBehaviour
{
    public HingeJoint hj;
    public Vector3 axisVector;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
        Vector3 target = axisVector * speed;
        JointMotor motor = hj.motor;
        motor.targetVelocity = speed; 
        hj.motor = motor;  
    }
}
