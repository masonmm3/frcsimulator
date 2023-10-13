using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armSlider : MonoBehaviour
{
    public GameObject segment1, segment2, endpoint;
    public int speed;
    public string inkey;
    public string outkey;
    public double min, max;
    public float pos;

    // Update is called once per frame
    void Update()
    {
        pos = (endpoint.transform.localPosition.x + 2) * 1000;

        if(Input.GetKey(inkey) && pos > min){
            endpoint.transform.Translate(Vector3.left * 2 * speed * Time.deltaTime, this.transform);
            segment1.transform.Translate(Vector3.left * 2 * speed * Time.deltaTime);
            segment2.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if(Input.GetKey(outkey) && pos < max){
            // Spin the object around the target at 20 degrees/second.
            endpoint.transform.Translate(Vector3.left * 2 * -speed * Time.deltaTime, this.transform);
            segment1.transform.Translate(Vector3.left * 2 * -speed * Time.deltaTime);
            segment2.transform.Translate(Vector3.left * -speed * Time.deltaTime);
        }
    }
}
