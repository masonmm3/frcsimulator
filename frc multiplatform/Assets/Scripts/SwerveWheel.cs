using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveWheel : MonoBehaviour
{
    private WheelCollider Wheel;
    public float WS;
    public float WA;
    public Vector3 Position;
    // Start is called before the first frame update
    void Start()
    {
        WS = 0;
        WA = 0;
        Wheel = GetComponent<WheelCollider>();
        Position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Wheel.steerAngle = WA;
        Wheel.motorTorque = WS;

        transform.localRotation = Quaternion.Euler( new Vector3(0, WA+90, 0));
    }
}
