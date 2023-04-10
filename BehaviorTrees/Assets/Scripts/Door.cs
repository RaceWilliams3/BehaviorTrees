using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public bool locked;

    public Vector3 closedRot = new Vector3(0, 0, 0);
    public Vector3 openRot = new Vector3(0, -135, 0);

    void Awake()
    {
        RotDoor();
    }

    public void RotDoor()
    {
        if (open == true)
        {
            transform.eulerAngles = openRot;
        } 
        else
        {
            transform.eulerAngles = closedRot;
        }
    }

    public void Open()
    {
        open = true;
        RotDoor();
        Debug.Log("opened :D");
    }
}
