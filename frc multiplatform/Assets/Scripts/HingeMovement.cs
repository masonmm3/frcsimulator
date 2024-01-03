using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeMovement : MonoBehaviour
{
    public ConfigurableJoint CJ;
    public float TargetAngle;
    public Vector3 AxisVector;
    // Start is called before the first frame update
    void Start()
    {
        CJ = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        CJ.targetRotation = Quaternion.AngleAxis(TargetAngle, AxisVector);
    }
}
