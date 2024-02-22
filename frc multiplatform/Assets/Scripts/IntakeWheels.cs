using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeWheels : MonoBehaviour
{
    public HingeJoint hj;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JointMotor intake = hj.motor;
        intake.targetVelocity = speed;
        hj.motor = intake;
    }
}
