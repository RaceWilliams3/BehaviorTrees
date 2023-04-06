using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = false;
    public bool locked = false;

    public void Open()
    {
        open = true;
        Debug.Log("opened :D");
    }
}
