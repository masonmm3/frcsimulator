using UnityEngine;

//Attach this script to a GameObject to rotate around the target position.
public class centerOfRotation : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    public GameObject target;
    public int speed;
    public string downkey;
    public string upkey;
    public int upangle;
    public int downangle;
    public float angle;
    public bool nolimit;

    void Update()
    {
        angle = transform.rotation.eulerAngles.z - 180;
        if(Input.GetKey(downkey) && (angle < downangle || nolimit)){
            // Spin the object around the target at 20 degrees/second.
            transform.RotateAround(target.transform.position, transform.forward, speed * Time.deltaTime);
        }

        if(Input.GetKey(upkey) && (angle > upangle || nolimit)){
            // Spin the object around the target at 20 degrees/second.
            transform.RotateAround(target.transform.position, transform.forward, -speed * Time.deltaTime);
        }
    }
}