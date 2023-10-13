using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intake : MonoBehaviour
{
    public int speed;
    public Transform intake;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, intake.position) < .2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, intake.position, speed * Time.deltaTime);
        }
    }
}
