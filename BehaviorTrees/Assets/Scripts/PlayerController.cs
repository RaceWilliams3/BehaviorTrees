using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (Input.GetKey("a"))
        {
            //rb.AddForce(-speed, 0, 0, ForceMode.Force);
            this.GetComponent<Transform>().position += new Vector3(-speed*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            //rb.AddForce(speed, 0, 0, ForceMode.Force);
            this.GetComponent<Transform>().position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w"))
        {
            //rb.AddForce(0, 0, speed, ForceMode.Force);
            this.GetComponent<Transform>().position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            //rb.AddForce(0, 0, -speed, ForceMode.Force);
            this.GetComponent<Transform>().position += new Vector3(0, 0, -speed * Time.deltaTime);
        }

    }
}
