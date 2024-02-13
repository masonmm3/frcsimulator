using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intake1 : MonoBehaviour
{
    public ConfigurableJoint cj;
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
        cj.targetAngularVelocity = target;
    }
}
